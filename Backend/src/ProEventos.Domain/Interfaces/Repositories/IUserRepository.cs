using ProEventos.Domain.Identity;
using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserNameAsync(string name);
        Task<bool> AddAsync(User entity);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(User entity);
    }
}
