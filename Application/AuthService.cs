using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAccountRepository<Account> _accountRepository;

        public AuthService(IConfiguration config, IAccountRepository<Account> accountRepository)
        {
            _config = config;
            _accountRepository = accountRepository;
        }

        private string GenerateJwtToken(Account user)
        {
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user is Staff s ? s.Role.ToString() : "Requester")
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public async Task<Result<string>> LoginAsync(LoginRequestDto request)
        {
            Account? user = await _accountRepository.GetAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Result<string>.Fail("Invalid email or password.");

            if (user is Requester requester)
            {
                if (requester.AdmissionStatus == AdmissionStatus.Pending)
                    return Result<string>.Fail("Requester account is not accepted yet.");

                if (requester.AdmissionStatus == AdmissionStatus.Rejected)
                    return Result<string>.Fail("Requester account has been rejected.");
            }

            string token = GenerateJwtToken(user);
            return Result<string>.Ok(token);
        }

        public async Task<Result<AccountResponseDto>> RegisterRequesterAsync(RequesterRegistrationRequestDto request)
        {
            Account? existingUser = await _accountRepository.GetAsync(request.Email);
            if (existingUser != null)
                return Result<AccountResponseDto>.Fail("This email is already used.");

            Requester user = new Requester
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Requester,
                AdmissionStatus = AdmissionStatus.Pending
            };

            Account addedUser = await _accountRepository.AddAsync(user);

            AccountResponseDto response = new AccountResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Result<AccountResponseDto>.Ok(response);
        }
    }
}
