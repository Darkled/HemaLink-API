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
        public async Task<StaffResponseDto> RegisterModeratorAsync(ModeratorRegistrationRequestDto request)
        {
            if (await _accountRepository.GetAsync(request.Email) != null)
            {
                throw new InvalidOperationException("This email is already used.");
            }

            Staff user = new Staff
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Moderator
            };
            await _accountRepository.AddAsync(user);

            return new StaffResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<StaffResponseDto> PromoteModeratorAsync(ModeratorPromotionDto request)
        {
            Account? user = await _accountRepository.GetAsync(request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (user is not Staff staffUser)
            {
                throw new InvalidOperationException("User is not a moderator.");
            }

            if (staffUser.Role == Role.Admin)
            {
                throw new InvalidOperationException("User is already admin.");
            }

            staffUser.Role = Role.Admin;

            await _accountRepository.UpdateAsync(staffUser);

            return new StaffResponseDto
            {
                Id = staffUser.Id,
                Name = staffUser.Name,
                Email = staffUser.Email,
                Role = staffUser.Role.ToString()
            };

        }

        public async Task<List<AccountResponseDto>> GetAllUsersAsync(Role? role)
        {
            List<Account> users = role.HasValue
                ? await _accountRepository.GetAllAsync(role.Value)
                : await _accountRepository.GetAllAsync();

            return users.Select(u => new AccountResponseDto
            {
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();
        }


        public async Task<AccountResponseDto?> GetUserAsync(string email)
        {
            Account? user = await _accountRepository.GetAsync(email);

            if (user == null)
                return null;

            return new AccountResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<bool> DeleteAccountAsync(string email)
        {
            var user = await _accountRepository.GetAsync(email);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            user.IsActive = false;

            await _accountRepository.UpdateAsync(user);

            return true;
        }
    }
}