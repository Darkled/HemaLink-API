using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Models.Enums;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<StaffResponseDto> RegisterModeratorAsync(ModeratorRegistrationRequestDto request);
        Task<StaffResponseDto> PromoteModeratorAsync(ModeratorPromotionDto request);
        Task<List<AccountResponseDto>> GetAllUsersAsync(Role? role);
        Task<AccountResponseDto?> GetUserAsync(string email);
        Task<bool> DeleteAccountAsync(string email);


    }
}
