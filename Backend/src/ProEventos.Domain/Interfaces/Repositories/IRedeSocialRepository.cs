using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IRedeSocialRepository : IBaseRepository<RedeSocial>
    {
        Task<RedeSocial[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId);
    }
}
