using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IModeratorService
    {
        Task<Result<RequesterResponseDto>> ValidateRequesterAsync(int requesterId, bool accept);
        Task<Result<List<RequesterResponseDto>>> GetPendingRequestersAsync();
        Task<Result<List<RequesterResponseDto>>> GetAllRequestersAsync();
        Task<Result<BloodRequestResponseDto>> AddBloodRequestAsync(BloodRequestRequestDto bloodRequest, int id);
        Task<Result<BloodRequestResponseDto>> UpdateBloodRequestAsync(int requestId, BloodRequestRequestDto bloodRequest);
        Task<Result<BloodRequestResponseDto>> CancelBloodRequestAsync(int requestId);
        Task<Result<List<DonorResponseDto>>> GetDonorsFromBloodRequestAsync(int requestId);
    }
}