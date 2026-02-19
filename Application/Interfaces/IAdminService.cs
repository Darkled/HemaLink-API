using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<Result<AccountResponseDto>> RegisterModeratorAsync(ModeratorRegistrationRequestDto request);
        Task<Result<AccountResponseDto>> PromoteModeratorAsync(ModeratorPromotionDto request);
        Task<Result<List<AccountResponseDto>>> GetAllUsersAsync(Role? role);
        Task<Result<AccountResponseDto>> GetUserAsync(int id);
        Task<Result<AccountResponseDto>> GetUserAsync(string email);
        Task<Result<bool>> DeleteAccountAsync(int id, int currentId);
    }
}
