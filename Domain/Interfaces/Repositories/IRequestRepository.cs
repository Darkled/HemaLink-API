using Domain.Models;
using Domain.Models.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IRequestRepository<T> : IBaseRepository<T> where T : BloodRequest
    {
        Task<List<T>> GetByRequesterIdAsync(int requesterId);
    }
}
