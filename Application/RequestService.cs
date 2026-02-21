using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;

namespace Application
{
    public class RequestService
    {
        private readonly IRequestRepository<BloodRequest> _requestRepository;
        public RequestService(IRequestRepository<BloodRequest> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        //public async Task<List<BloodRequest>> GetOpenRequestsAsync()
        //{
        //    return await _requestRepository.GetAsync();
        //}

        //public async Task<List<BloodRequest>> GetOpenRequestsByBloodTypeAsync(BloodType? bloodType)
        //{
        //    return bloodType == null? 
        //        await _requestRepository.GetAsync() :
        //        await _requestRepository.GetAsync(bloodType.Value);
        //}
    }
}
