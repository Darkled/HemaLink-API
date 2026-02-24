using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RequesterRepository : AccountRepository<Requester>, IRequesterRepository
    {
        public RequesterRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Requester>> GetPendingAsync()
        {
            return await _dbContext.Requesters.Where(r => r.AdmissionStatus == AdmissionStatus.Pending).ToListAsync();
        }

        public async Task<List<Donor>> GetDonorsByRequesterIdAsync(int requesterId)
        {
            return await _dbContext.BloodRequests
                .Where(br => br.RequesterId == requesterId)
                .SelectMany(br => br.Appointments)
                .Select(a => a.Donor)
                .Distinct()
                .ToListAsync();
        }
    }
}
