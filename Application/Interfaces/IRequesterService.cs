using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IRequesterService
    {
        Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto bloodRequest, int id);
        Task<Result<List<BloodRequestResponseDto>>> GetBloodRequestsByRequesterIdAsync(int id, List<RequestStatus> statuses);
        Task<Result<BloodRequestResponseDto>> UpdateBloodRequestAsync(int requestId, int? requesterId, BloodRequestRequestDto bloodRequest, bool bypassOwnerCheck = false);
        Task<Result<BloodRequestResponseDto>> CancelBloodRequestAsync(int requestId, int? requesterId, bool bypassOwnerCheck = false);
        Task<Result<List<DonorResponseDto>>> GetDonorsFromBloodRequestAsync(int requestId, int? requesterId, bool bypassOwnerCheck = false);
        Task<Result<List<DonorResponseDto>>> GetDonorsFromRequesterAsync(int requesterId);
    }
}