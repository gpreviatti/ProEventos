using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string name);
        Task<UserDto> SalvarAsync(UserDto UserDto);
        Task<bool> DeletarAsync(int UserId);
        Task<UserDto> GetByIdAsync(int UserId);
        Task<UserDto> GetByNameAsync(string name);
    }
}
