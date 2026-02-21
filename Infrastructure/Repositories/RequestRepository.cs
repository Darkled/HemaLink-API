using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RequestRepository<T> : BaseRepository<T>, IRequestRepository<T> where T : BloodRequest
    {
        public RequestRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<T>> GetByRequesterIdAsync(int requesterId)
        {
            return await _dbContext.Set<T>()
                .Include(br => br.Requester)
                .Where(br => br.RequesterId == requesterId)
                .OrderByDescending(br => br.RequestedOn)
                .ToListAsync();
        }
    }
}
