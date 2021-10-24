using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> GetAllPalestrantesAsync();
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
    }
}
