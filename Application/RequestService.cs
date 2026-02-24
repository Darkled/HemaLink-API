using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace Application
{
    public class RequestService : IRequestService
    {
        private readonly IBloodRequestRepository _requestRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public RequestService(IBloodRequestRepository requestRepository,
                                IDonorRepository donorRepository,
                                IBloodRequestRepository bloodRequestRepository,
                                IAppointmentRepository appointmentRepository,
                                IUnitOfWork unitOfWork,
                                IEmailService emailService)
        {
            _requestRepository = requestRepository;
            _donorRepository = donorRepository;
            _bloodRequestRepository = bloodRequestRepository;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<List<BloodRequestResponseDto>>> GetOpenRequestsAsync(List<BloodType> bloodTypes)
        {
            List<BloodRequest> requests;
            if (!bloodTypes.Any())
            {
                requests = await _requestRepository.GetActiveAsync();
            }
            else
            {
                requests = await _requestRepository.GetActiveByBloodTypeAsync(bloodTypes);
            }

            List<BloodRequestResponseDto> requestForResponse = requests.Select(requests => new BloodRequestResponseDto
            {
                RequestId = requests.Id,
                RequesterName = requests.Requester!.Name,
                BloodTypesNeeded = requests.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                RequestDate = requests.RequestDate,
                TargetUnits = requests.TargetUnits,
                RemainingUnits = requests.RemainingUnits,
                RequestStatus = requests.RequestStatus.ToString(),
                Address = requests.Address
            }).ToList();

            return Result<List<BloodRequestResponseDto>>.Ok(requestForResponse);
        }

        public async Task<Result<AppointBloodRequestResponseDto>> AppointBloodRequestAsync(AppointBloodRequestRequestDto appointBloodRequestDto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Donor? donor = await _donorRepository.GetByEmailAsync(appointBloodRequestDto.DonorEmail);

                if (donor == null)
                {
                    donor = new Donor
                    {
                        Name = appointBloodRequestDto.DonorName,
                        Email = appointBloodRequestDto.DonorEmail,
                        Phone = appointBloodRequestDto.DonorPhone
                    };
                    await _donorRepository.AddWithoutSavingAsync(donor);
                }

                BloodRequest? bloodRequest = await _bloodRequestRepository.GetByIdWithRequesterAsync(appointBloodRequestDto.BloodRequestId);

                if (bloodRequest == null) return Result<AppointBloodRequestResponseDto>.Fail("Blood request not found.");
                if (bloodRequest.RequestStatus != RequestStatus.Open) return Result<AppointBloodRequestResponseDto>.Fail("Blood request is not active.");
                if (bloodRequest.RemainingUnits <= 0) return Result<AppointBloodRequestResponseDto>.Fail("No remaining units available.");

                if (donor.Id != 0)
                {
                    bool alreadyExists = await _appointmentRepository.ExistsAsync(donor.Id, bloodRequest.Id);
                    if (alreadyExists) return Result<AppointBloodRequestResponseDto>.Fail("You already appointed this request.");
                }

                bloodRequest.RemainingUnits--;
                if (bloodRequest.RemainingUnits == 0) bloodRequest.RequestStatus = RequestStatus.Completed;

                await _bloodRequestRepository.UpdateWithoutSavingAsync(bloodRequest);

                byte[] bytes = RandomNumberGenerator.GetBytes(32);
                Appointment appointment = new Appointment
                {
                    Donor = donor,
                    BloodRequestId = bloodRequest.Id,
                    CancellationToken = WebEncoders.Base64UrlEncode(bytes)
                };

                await _appointmentRepository.AddWithoutSavingAsync(appointment);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                AppointBloodRequestResponseDto response = new AppointBloodRequestResponseDto
                {
                    BloodRequesterName = bloodRequest.Requester!.Name,
                    BloodRequestAdress = bloodRequest.Address,
                    RequestDate = bloodRequest.RequestDate,
                    DonorName = donor.Name,
                    DonorEmail = donor.Email,
                    DonorPhone = donor.Phone,
                };

                try
                {
                    await _emailService.SendReservationEmailAsync(
                        donor.Email,
                        donor.Name,
                        bloodRequest.Requester.Name,
                        bloodRequest.Address,
                        bloodRequest.RequestDate,
                        bloodRequest.Id,
                        appointment.CancellationToken!
                        );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error de SendGrid: {ex.Message}");
                }

                return Result<AppointBloodRequestResponseDto>.Ok(response);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<bool>> CancelAppointmentAsync(int requestId, string cancellationToken)
        {
            BloodRequest? bloodRequest = await _bloodRequestRepository.GetByIdWithDonorsAndRequesterAsync(requestId);
            if (bloodRequest == null)
                return Result<bool>.Fail("Blood request not found.");
            if (bloodRequest.RequestStatus != RequestStatus.Open)
                return Result<bool>.Fail("Blood request is not active.");

            Appointment? appointment = bloodRequest.Appointments.FirstOrDefault(ap => ap.CancellationToken == cancellationToken);
            if (appointment == null)
                return Result<bool>.Fail("Invalid cancellation token.");

            if (appointment.IsCancelled)
                return Result<bool>.Fail("Appointment is already cancelled.");

            bloodRequest.RemainingUnits++;
            if (bloodRequest.RequestStatus == RequestStatus.Completed)
                bloodRequest.RequestStatus = RequestStatus.Open;

            appointment.IsCancelled = true;
            await _bloodRequestRepository.UpdateAsync(bloodRequest);

            return Result<bool>.Ok(true);
        }
    }
}
