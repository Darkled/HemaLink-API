using Application.Interfaces;
using Application.Models.Responses;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;

namespace Application
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository<BloodRequest> _requestRepository;
        public RequestService(IRequestRepository<BloodRequest> requestRepository)
        {
            _requestRepository = requestRepository;
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
                RequesterName = requests.Requester!.Name,
                BloodTypesNeeded = requests.BloodTypesNeeded?.Select(bt => bt.ToString()).ToList(),
                RequestDate = requests.RequestDate,
                TargetUnits = requests.TargetUnits,
                RemainingUnits = requests.RemainingUnits,
                RequestStatus = requests.RequestStatus.ToString()
            }).ToList();

            return Result<List<BloodRequestResponseDto>>.Ok(requestForResponse);
        }

        //public async Task<List<BloodRequest>> GetOpenRequestsByBloodTypeAsync(BloodType? bloodType)
        //{
        //    return bloodType == null? 
        //        await _requestRepository.GetAsync() :
        //        await _requestRepository.GetAsync(bloodType.Value);
        //}
    }
}
