using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserRepositories
{
    public class AccountRepository<T> : BaseRepository<T>, IAccountRepository<T> where T : Account
    {
        public AccountRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<T?> GetAsync(string email)
        {
            return await _dbContext.Set<T>()
                .Where(u => u.IsActive)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>()
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(string role)
        {
            return await _dbContext.Set<T>()
                .Where(u => u.IsActive)
                .ToListAsync();
        }

    }
}
