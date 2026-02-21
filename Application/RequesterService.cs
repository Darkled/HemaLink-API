using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class RequesterService : IRequesterService
    {

        private readonly IRequestRepository<BloodRequest> _requestRepository;
        private readonly IAccountRepository<Account> _authRepository;
        public RequesterService(IRequestRepository<BloodRequest> requestRepository, IAccountRepository<Account> accountRepository)
        {
            _requestRepository = requestRepository;
            _authRepository = accountRepository;
        }

        public async Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto dto, int id)
        {
            BloodRequest bloodRequest = new BloodRequest
            {
                RequesterId = id,
                BloodTypesNeeded = dto.BloodTypesNeeded?.ToList(),
                TargetUnits = dto.TargetUnits,
                RemainingUnits = dto.TargetUnits,
                RequestedOn = DateTime.UtcNow,
                RequestStatus = RequestStatus.Open
            };

            BloodRequest created = await _requestRepository.AddAsync(bloodRequest);

            Account requester = (await _authRepository.GetAsync(id))!;

            BloodRequestResponseDto responseDto = new BloodRequestResponseDto
            {
                RequesterName = requester!.Name,
                BloodTypesNeeded = created.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                TargetUnits = created.TargetUnits,
                RemainingUnits = created.RemainingUnits,
                RequestedOn = created.RequestedOn,
                RequestStatus = created.RequestStatus.ToString()
            };

            return Result<BloodRequestResponseDto>.Ok(responseDto);
        }

        public async Task<Result<List<BloodRequestResponseDto>>> GetBloodRequestsByRequesterIdAsync(int requesterId)
        {
            List<BloodRequest> bloodRequests = await _requestRepository
                .GetByRequesterIdAsync(requesterId);

            List<BloodRequestResponseDto> response = bloodRequests.Select(br => new BloodRequestResponseDto
            {
                RequesterName = br.Requester!.Name,
                TargetUnits = br.TargetUnits,
                RemainingUnits = br.RemainingUnits,
                RequestStatus = br.RequestStatus.ToString(),
                RequestedOn = br.RequestedOn
            }).ToList();

            return Result<List<BloodRequestResponseDto>>.Ok(response);
        }
    }
}
