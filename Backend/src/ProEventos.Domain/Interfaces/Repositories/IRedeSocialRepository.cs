using System.Threading.Tasks;
using ProEventos.Domain;

public interface IRedeSocialRepository : IBaseRepository<RedeSocial>
{
    Task<RedeSocial[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId);
    Task<RedeSocial> GetRedeSocialByIdAsync(int redeSocialId);
}
