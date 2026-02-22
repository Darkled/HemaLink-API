using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IRequestService
    {
        Task<Result<List<BloodRequestResponseDto>>> GetOpenRequestsAsync(List<BloodType> bloodTypes);
        Task<Result<AppointBloodRequestResponseDto>> AppointBloodRequestAsync(AppointBloodRequestRequestDto appointBloodRequestDto);
    }
}
