using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;

namespace Application
{
    public class ModeratorService : IModeratorService
    {
        private readonly IAccountRepository<Account> _accountRepository;
        private readonly IRequesterRepository _requesterRepository;
        private readonly IRequesterService _requesterService;
        private readonly IEmailService _emailService;
        private readonly IDonorRepository _donorRepository;
        public ModeratorService(IRequesterRepository requesterRepository, IRequesterService requesterService, IAccountRepository<Account> accountRepository, IEmailService emailService, IDonorRepository donorRepository)
        {
            _requesterRepository = requesterRepository;
            _requesterService = requesterService;
            _accountRepository = accountRepository;
            _emailService = emailService;
            _donorRepository = donorRepository;
        }
        public async Task<Result<RequesterResponseDto>> ValidateRequesterAsync(int requesterId, bool accept)
        {
            Requester? requester = await _requesterRepository.GetAsync(requesterId);
            if (requester == null)
            {
                return Result<RequesterResponseDto>.Fail("Requester not found.");
            }
            if (requester.AdmissionStatus != AdmissionStatus.Pending)
            {
                return Result<RequesterResponseDto>.Fail("Requester has already been processed.");
            }
            requester.AdmissionStatus = accept ? AdmissionStatus.Accepted : AdmissionStatus.Rejected;
            await _requesterRepository.UpdateAsync(requester);

            if (accept)
                await _emailService.SendAcceptingNotificationEmailAsync(requester);
            else
                await _emailService.SendRejectingNotificationEmailAsync(requester);

            var responseDto = new RequesterResponseDto
            {
                Id = requester.Id,
                Name = requester.Name,
                Email = requester.Email,
                AdmissionStatus = requester.AdmissionStatus
            };

            return Result<RequesterResponseDto>.Ok(responseDto);
        }

        public async Task<Result<List<RequesterResponseDto>>> GetPendingRequestersAsync()
        {
            List<Requester> pendingRequesters = await _requesterRepository.GetPendingAsync();
            List<RequesterResponseDto> responseDtos = pendingRequesters.Select(r => new RequesterResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email,
                AdmissionStatus = r.AdmissionStatus
            }).ToList();
            return Result<List<RequesterResponseDto>>.Ok(responseDtos);
        }

        public async Task<Result<List<RequesterResponseDto>>> GetAllRequestersAsync()
        {
            List<Requester> requesters = await _requesterRepository.GetAllAsync();
            List<RequesterResponseDto> responseDtos = requesters.Select(r => new RequesterResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email,
                AdmissionStatus = r.AdmissionStatus
            }).ToList();
            return Result<List<RequesterResponseDto>>.Ok(responseDtos);
        }

        public async Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto bloodRequest, int id)
        {
            Account? account = await _accountRepository.GetAsync(id);
            if (account == null)
            {
                return Result<BloodRequestResponseDto>.Fail("Requester not found");
            }
            if (account.Role != Role.Requester)
            {
                return Result<BloodRequestResponseDto>.Fail("Account is not a requester");
            }
            return await _requesterService.AddBloodRequestAsync(bloodRequest, id);
        }

        public async Task<Result<BloodRequestResponseDto>> UpdateBloodRequestAsync(int requestId, BloodRequestRequestDto bloodRequest)
        {
            return await _requesterService.UpdateBloodRequestAsync(requestId, null, bloodRequest, true);
        }
        public async Task<Result<BloodRequestResponseDto>> CancelBloodRequestAsync(int requestId)
        {
            return await _requesterService.CancelBloodRequestAsync(requestId, null, true);
        }

        public async Task<Result<List<DonorResponseDto>>> GetDonorsFromBloodRequestAsync(int requestId)
        {
            return await _requesterService.GetDonorsFromBloodRequestAsync(requestId, null, true);
        }

        public async Task<Result<List<DonorResponseDto>>> GetAllDonorsAsync()
        {
            List<Donor> donors = await _donorRepository.GetAllAsync();
            List<DonorResponseDto> responseDtos = donors.Select(d => new DonorResponseDto
            {
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone
            }).ToList();

            return Result<List<DonorResponseDto>>.Ok(responseDtos);
        }
    }
}
