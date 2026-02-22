using Domain.Models;
using Domain.Models.Enums;

namespace Domain.Interfaces.Repositories
{
    public interface IAccountRepository<T> : IBaseRepository<T> where T : Account
    {
        Task<T?> GetAsync(string email);
        Task<List<T>> GetAllAsync(Role role);
    }

}
