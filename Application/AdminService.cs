using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;

namespace Application
{
    public class AdminService : IAdminService
    {
        private readonly IAccountRepository<Account> _accountRepository;

        public AdminService(IAccountRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<AccountResponseDto>> RegisterModeratorAsync(ModeratorRegistrationRequestDto request)
        {
            var existingUser = await _accountRepository.GetAsync(request.Email);
            if (existingUser != null)
                return Result<AccountResponseDto>.Fail("This email is already used.");

            var user = new Staff
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Moderator
            };

            await _accountRepository.AddAsync(user);

            var response = new AccountResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Result<AccountResponseDto>.Ok(response);
        }

        public async Task<Result<AccountResponseDto>> PromoteModeratorAsync(ModeratorPromotionDto request)
        {
            var user = await _accountRepository.GetAsync(request.Email);

            if (user == null)
                return Result<AccountResponseDto>.Fail("User not found.");

            if (user is not Staff staffUser)
                return Result<AccountResponseDto>.Fail("User is not a moderator.");

            if (staffUser.Role == Role.Admin)
                return Result<AccountResponseDto>.Fail("User is already admin.");

            staffUser.Role = Role.Admin;
            await _accountRepository.UpdateAsync(staffUser);

            var response = new AccountResponseDto
            {
                Id = staffUser.Id,
                Name = staffUser.Name,
                Email = staffUser.Email,
                Role = staffUser.Role.ToString()
            };

            return Result<AccountResponseDto>.Ok(response);
        }

        public async Task<Result<List<AccountResponseDto>>> GetAllUsersAsync(Role? role)
        {
            List<Account> users = role.HasValue
                ? await _accountRepository.GetAllAsync(role.Value)
                : await _accountRepository.GetAllAsync();

            List<AccountResponseDto> response = users.Select(u => new AccountResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();

            return Result<List<AccountResponseDto>>.Ok(response);
        }

        public async Task<Result<AccountResponseDto>> GetUserAsync(int id)
        {
            var user = await _accountRepository.GetAsync(id);

            if (user == null)
                return Result<AccountResponseDto>.Fail("User not found.");

            var response = new AccountResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Result<AccountResponseDto>.Ok(response);
        }

        public async Task<Result<AccountResponseDto>> GetUserAsync(string email)
        {
            var user = await _accountRepository.GetAsync(email);

            if (user == null)
                return Result<AccountResponseDto>.Fail("User not found.");

            var response = new AccountResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return Result<AccountResponseDto>.Ok(response);
        }

        public async Task<Result<bool>> DeleteAccountAsync(int id)
        {
            var user = await _accountRepository.GetAsync(id);

            if (user == null)
                return Result<bool>.Fail("User not found.");

            user.IsActive = false;
            await _accountRepository.UpdateAsync(user);

            return Result<bool>.Ok(true);
        }
    }
}
