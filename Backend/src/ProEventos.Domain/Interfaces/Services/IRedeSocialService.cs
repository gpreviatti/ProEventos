using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId, int redeSocialId);
        Task<RedeSocialDto> GetRedeSocialByIdAsync(int redeSocialId);
        Task<RedeSocialDto> Salvar(int palestranteId, RedeSocialDto redeSocial);
        Task<bool> Deletar(int id);
    }
}
