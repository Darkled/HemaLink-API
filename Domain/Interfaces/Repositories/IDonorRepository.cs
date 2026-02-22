using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IDonorRepository : IBaseRepository<Donor>
    {
        Task<Donor?> GetByEmailAsync(string email);
    }
}
