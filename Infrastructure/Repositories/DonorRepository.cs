using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DonorRepository : BaseRepository<Donor>, IDonorRepository
    {
        public DonorRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Donor?> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<Donor>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
