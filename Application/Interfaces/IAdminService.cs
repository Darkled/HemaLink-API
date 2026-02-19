using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<Result<StaffResponseDto>> RegisterModeratorAsync(ModeratorRegistrationRequestDto request);
        Task<Result<StaffResponseDto>> PromoteModeratorAsync(ModeratorPromotionDto request);
        Task<Result<List<AccountResponseDto>>> GetAllUsersAsync(Role? role);
        Task<Result<AccountResponseDto>> GetUserAsync(string email);
        Task<Result<bool>> DeleteAccountAsync(string email);
    }
}
