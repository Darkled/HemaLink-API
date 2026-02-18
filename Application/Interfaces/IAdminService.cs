using Application.Models.Requests;
using Application.Models.Responses;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<StaffResponseDto> RegisterModeratorAsync(ModeratorRegistrationRequestDto request);
        Task<StaffResponseDto> PromoteModeratorAsync(ModeratorPromotionDto request);
        Task<List<AccountListDto>> GetUsersByRoleAsync(string role);
        Task<AccountListDto?> GetUserByEmailAsync(string email);
        Task<bool> DeleteAccountAsync(string email);


    }
}
