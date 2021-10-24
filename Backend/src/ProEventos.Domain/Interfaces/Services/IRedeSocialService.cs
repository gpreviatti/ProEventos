using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId);
        Task<RedeSocialDto> GetRedeSocialByIdAsync(int redeSocialId);
        Task<RedeSocialDto> SalvarAsync(RedeSocialDto redeSocial);
        Task<bool> DeletarAsync(int id);
    }
}
