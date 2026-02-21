using Application.Models.Requests;
using Application.Models.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginRequestDto request);
        Task<Result<AccountResponseDto>> RegisterRequesterAsync(RequesterRegistrationRequestDto request);
    }

}
