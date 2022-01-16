using ProEventos.Domain.Identity;
using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserNameAsync(string name);
    }
}