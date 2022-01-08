using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId);
        Task<RedeSocialDto> GetRedeSocialByIdAsync(int redeSocialId);
        Task<RedeSocialDto> SalvarAsync(RedeSocialDto redeSocial);
        Task<bool> DeletarAsync(int id);
    }
}
