using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<bool> ExistsAsync(int donorId, int bloodRequestId);
    }
}
