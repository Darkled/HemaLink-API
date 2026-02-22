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

        public async Task<List<T>> GetActiveAsync()
        {
            return await _dbContext.Set<T>()
                .Include(br => br.Requester)
                .Where(br => br.RequestStatus == RequestStatus.Open)
                .OrderByDescending(br => br.RequestDate)
                .ToListAsync();
        }

        public async Task<List<T>> GetActiveByBloodTypeAsync(List<BloodType> bloodTypes)
        {
            return await _dbContext.Set<T>()
                .Include(br => br.Requester)
                .Where(br => br.RequestStatus == RequestStatus.Open)
                .Where(br => br.BloodTypesNeeded!.Any(btn => bloodTypes.Contains(btn)))
                .ToListAsync();
        }

        public async Task<List<T>> GetByRequesterIdAsync(int requesterId)
        {
            return await _dbContext.Set<T>()
                .Include(br => br.Requester)
                .Where(br => br.RequesterId == requesterId)
                .OrderByDescending(br => br.RequestDate)
                .ToListAsync();
        }
    }
}
