using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IRequesterRepository : IAccountRepository<Requester>
    {
        Task<List<Requester>> GetPendingAsync();
        Task<List<Donor>> GetDonorsByRequesterIdAsync(int requesterId);
    }
}
