using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginRequestDto request);
        Task<Result<string>> RegisterRequesterAsync(RequesterRegistrationRequestDto request);
    }

}
