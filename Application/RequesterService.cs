using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class RequesterService : IRequesterService
    {

        private readonly IBloodRequestRepository _requestRepository;
        private readonly IAccountRepository<Account> _authRepository;
        private readonly IEmailService _emailService;
        public RequesterService(IBloodRequestRepository requestRepository, IAccountRepository<Account> accountRepository, IEmailService emailService)
        {
            _requestRepository = requestRepository;
            _authRepository = accountRepository;
            _emailService = emailService;
        }

        public async Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto dto, int id)
        {
            BloodRequest bloodRequest = new BloodRequest
            {
                RequesterId = id,
                BloodTypesNeeded = dto.BloodTypesNeeded?.ToList(),
                TargetUnits = dto.TargetUnits,
                Address = dto.Address,
                RemainingUnits = dto.TargetUnits,
                RequestDate = dto.RequestDate,
                RequestStatus = RequestStatus.Open
            };

            BloodRequest created = await _requestRepository.AddAsync(bloodRequest);

            Account requester = (await _authRepository.GetAsync(id))!;

            BloodRequestResponseDto responseDto = new BloodRequestResponseDto
            {
                RequestId = created.Id,
                RequesterName = requester!.Name,
                BloodTypesNeeded = created.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                TargetUnits = created.TargetUnits,
                RemainingUnits = created.RemainingUnits,
                RequestDate = created.RequestDate,
                RequestStatus = created.RequestStatus.ToString(),
                Address = created.Address
            };

            return Result<BloodRequestResponseDto>.Ok(responseDto);
        }

        public async Task<Result<List<BloodRequestResponseDto>>> GetBloodRequestsByRequesterIdAsync(int requesterId, List<RequestStatus> statuses)
        {
            List<BloodRequest> bloodRequests = await _requestRepository
                .GetByRequesterIdAsync(requesterId, statuses);

            List<BloodRequestResponseDto> response = bloodRequests.Select(br => new BloodRequestResponseDto
            {
                RequestId = br.Id,
                RequesterName = br.Requester!.Name,
                TargetUnits = br.TargetUnits,
                BloodTypesNeeded = br.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                RemainingUnits = br.RemainingUnits,
                RequestStatus = br.RequestStatus.ToString(),
                RequestDate = br.RequestDate,
                Address = br.Address,
            }).ToList();

            return Result<List<BloodRequestResponseDto>>.Ok(response);
        }

        public async Task<Result<BloodRequestResponseDto>> UpdateBloodRequestAsync(int requestId, int? requesterId, BloodRequestRequestDto bloodRequest, bool bypassOwnerCheck = false)
        {
            BloodRequest? existingRequest = await _requestRepository.GetByIdWithRequesterAsync(requestId);
            if (existingRequest == null)
                return Result<BloodRequestResponseDto>.Fail("Blood request not found.");
            if (!bypassOwnerCheck && existingRequest.RequesterId != requesterId)
                return Result<BloodRequestResponseDto>.Fail("Unauthorized to update this blood request.");

            existingRequest.BloodTypesNeeded = bloodRequest.BloodTypesNeeded?.ToList();
            existingRequest.TargetUnits = bloodRequest.TargetUnits;
            existingRequest.Address = bloodRequest.Address;
            existingRequest.RequestDate = bloodRequest.RequestDate;
            BloodRequest updated = await _requestRepository.UpdateAsync(existingRequest);
            BloodRequestResponseDto responseDto = new BloodRequestResponseDto
            {
                RequestId = updated.Id,
                RequesterName = updated.Requester!.Name,
                BloodTypesNeeded = updated.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                TargetUnits = updated.TargetUnits,
                RemainingUnits = updated.RemainingUnits,
                RequestDate = updated.RequestDate,
                RequestStatus = updated.RequestStatus.ToString(),
                Address = updated.Address
            };
            return Result<BloodRequestResponseDto>.Ok(responseDto);
        }

        public async Task<Result<bool>> DeleteBloodRequestAsync(int requestId, int? requesterId, bool bypassOwnerCheck = false)
        {
            BloodRequest? existingRequest = await _requestRepository.GetByIdWithRequesterAsync(requestId);
            if (existingRequest == null)
                return Result<bool>.Fail("Blood request not found.");
            if (!bypassOwnerCheck && existingRequest.RequesterId != requesterId)
                return Result<bool>.Fail("Unauthorized to delete this blood request.");
            if (existingRequest.RequestStatus == RequestStatus.Cancelled || existingRequest.RequestStatus == RequestStatus.Expired)
                return Result<bool>.Fail("Blood request is already deleted.");

            existingRequest.RequestStatus = RequestStatus.Cancelled;
            BloodRequest updated = await _requestRepository.UpdateAsync(existingRequest);

            BloodRequest requestWithDonors = (await _requestRepository.GetByIdWithDonorsAndRequesterAsync(requestId))!;
            requestWithDonors.Appointments.ForEach(a => _emailService
                .SendCancellationEmailAsync(a.Donor.Email, a.Donor.Name, requestWithDonors.Requester.Name, requestWithDonors.Address, requestWithDonors.RequestDate));

            return Result<bool>.Ok(true);
        }

        public async Task<Result<List<DonorResponseDto>>> GetDonorsFromBloodRequestAsync(int requestId, int? requesterId, bool bypassOwnerCheck = false)
        {
            BloodRequest? existingRequest = await _requestRepository.GetByIdWithRequesterAsync(requestId);
            if (existingRequest == null)
                return Result<List<DonorResponseDto>>.Fail("Blood request not found.");
            if (!bypassOwnerCheck && existingRequest.RequesterId != requesterId)
                return Result<List<DonorResponseDto>>.Fail("Unauthorized to view donors for this blood request.");

            BloodRequest? request = await _requestRepository.GetByIdWithDonorsAsync(requestId);
            if (request == null)
                return Result<List<DonorResponseDto>>.Fail("Blood request not found.");

            List<Donor> donors = request.Appointments.Select(a => a.Donor).ToList();
            List<DonorResponseDto> response = donors.Select(d => new DonorResponseDto
            {
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone
            }).ToList();
            return Result<List<DonorResponseDto>>.Ok(response);
        }
    }
}
