using Application.Models.Responses;
using Domain.Models;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IRequestService
    {
        Task<Result<List<BloodRequestResponseDto>>> GetOpenRequestsAsync(List<BloodType> bloodTypes);
    }
}
