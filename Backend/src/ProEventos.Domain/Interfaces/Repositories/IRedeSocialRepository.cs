using System.Threading.Tasks;
using ProEventos.Domain;

public interface IRedeSocialRepository : IBaseRepository<RedeSocial>
{
    Task<RedeSocial[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId, int redeSocialId);
    Task<RedeSocial> GetRedeSocialByIdAsync(int redeSocialId);
}
