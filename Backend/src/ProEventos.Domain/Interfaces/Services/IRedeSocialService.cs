using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface IRedeSocialService : IBaseService<RedeSocial>
    {
        Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId);
    }
}
