using Application.Models.Requests;
using Application.Models.Responses;

namespace Application.Interfaces
{
    public interface IRequesterService
    {
        Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto bloodRequest, int id);
        Task<Result<List<BloodRequestResponseDto>>> GetBloodRequestsByRequesterIdAsync(int id);
    }
}