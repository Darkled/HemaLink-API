using Application.Models.Requests;
using Application.Models.Responses;

namespace Application.Interfaces
{
    public interface IRequesterService
    {
        //Task<Result<List<BloodRequestResponseDto>>> GetAllBloodRequestsAsync();
        Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto bloodRequest, int id);
        Task<Result<List<BloodRequestResponseDto>>> GetBloodRequestsByRequesterIdAsync(int id);
        //Task<Result<BloodRequestResponseDto>> GetBloodRequestAsync(int id);
        //Task<Result<BloodRequestResponseDto>> RegisterBloodRequestAsync(BloodRequestRequestDto request);
    }
}