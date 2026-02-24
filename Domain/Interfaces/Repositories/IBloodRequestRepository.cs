using Domain.Models;
using Domain.Models.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IBloodRequestRepository : IBaseRepository<BloodRequest>
    {
        Task<List<BloodRequest>> GetActiveAsync();
        Task<BloodRequest?> GetByIdWithRequesterAsync(int id);
        Task<BloodRequest?> GetByIdWithDonorsAndRequesterAsync(int id);
        Task<BloodRequest?> GetByIdWithDonorsAsync(int id);
        Task<List<BloodRequest>> GetActiveByBloodTypeAsync(List<BloodType> bloodTypes);
        Task<List<BloodRequest>> GetByRequesterIdAsync(int requesterId, List<RequestStatus> statuses);
        Task<List<BloodRequest>> GetExpirableAsync();
        Task UpdateRangeAsync(List<BloodRequest> requests);
        Task<List<BloodRequest>> GetByStatusAsync(RequestStatus status);
    }
}
