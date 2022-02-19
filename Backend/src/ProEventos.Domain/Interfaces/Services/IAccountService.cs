using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> SalvarAsync(UserCreateDto UserDto);
        Task<UserUpdateDto> UpdateAsync(UserUpdateDto UserDto);
        Task<bool> DeletarAsync(int UserId);
        Task<UserDto> GetByIdAsync(int UserId);
        Task<UserDto> GetByUserNameAsync(string name);
        Task<bool> CheckUserPasswordAsync(UserLoginDto userLoginDto);
        Task<string> CreateToken(UserDto userDto);
    }
}
