using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> ExistsAsync(int donorId, int bloodRequestId)
        {
            return await _dbContext.Set<Appointment>()
                .AnyAsync(a => a.DonorId == donorId && a.BloodRequestId == bloodRequestId);
        }
    }
}
