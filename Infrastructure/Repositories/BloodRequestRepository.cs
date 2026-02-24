using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BloodRequestRepository : BaseRepository<BloodRequest>, IBloodRequestRepository
    {
        public BloodRequestRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<BloodRequest>> GetActiveAsync()
        {
            return await _dbContext.Set<BloodRequest>()
                .Include(br => br.Requester)
                .Where(br => br.RequestStatus == RequestStatus.Open)
                .OrderByDescending(br => br.RequestDate)
                .ToListAsync();
        }

        public async Task<BloodRequest?> GetByIdWithRequesterAsync(int id)
        {
            return await _dbContext.Set<BloodRequest>()
                .Include(br => br.Requester)
                .FirstOrDefaultAsync(br => br.Id == id);
        }

        public async Task<BloodRequest?> GetByIdWithDonorsAndRequesterAsync(int id)
        {
            return await _dbContext.Set<BloodRequest>()
                .Include(br => br.Appointments)
                    .ThenInclude(a => a.Donor)
                .Include(br => br.Requester)
                .FirstOrDefaultAsync(br => br.Id == id);
        }

        public async Task<List<BloodRequest>> GetActiveByBloodTypeAsync(List<BloodType> bloodTypes)
        {
            return await _dbContext.Set<BloodRequest>()
                .Include(br => br.Requester)
                .Where(br => br.RequestStatus == RequestStatus.Open)
                .Where(br => br.BloodTypesNeeded!.Any(btn => bloodTypes.Contains(btn)) || br.BloodTypesNeeded!.Count() == 0)
                .ToListAsync();
        }

        public async Task<List<BloodRequest>> GetByRequesterIdAsync(int requesterId, List<RequestStatus> statuses)
        {
            var query = _dbContext.BloodRequests
                .Include(br => br.Requester)
                .Where(br => br.RequesterId == requesterId);

            if (statuses != null && statuses.Any())
            {
                query = query.Where(br => statuses.Contains(br.RequestStatus));
            }

            return await query
                .OrderByDescending(br => br.RequestDate)
                .ToListAsync();
        }

        public async Task<BloodRequest?> GetByIdWithDonorsAsync(int id)
        {
            return await _dbContext.Set<BloodRequest>()
                .Include(br => br.Appointments)
                    .ThenInclude(a => a.Donor)
                .FirstOrDefaultAsync(br => br.Id == id);
        }

        public async Task<List<BloodRequest>> GetExpirableAsync()
        {
            return await _dbContext.Set<BloodRequest>()
                .Where(br => br.RequestStatus == RequestStatus.Open && br.RequestDate < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task UpdateRangeAsync(List<BloodRequest> requests)
        {
            _dbContext.Set<BloodRequest>().UpdateRange(requests);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BloodRequest>> GetByStatusAsync(RequestStatus status)
        {
            return await _dbContext.Set<BloodRequest>()
                .Where(br => br.RequestStatus == status)
                .ToListAsync();

        }
    }
}
