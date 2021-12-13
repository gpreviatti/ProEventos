using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> GetAllPaginatedAsync(int currentPage, int pageSize, string searchValue = "");
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
    }
}
