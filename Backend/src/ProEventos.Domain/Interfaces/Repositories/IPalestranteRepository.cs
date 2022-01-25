using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> Get();
        Task<Palestrante[]> GetAllPaginatedAsync(int currentPage, int pageSize, string searchValue = "");
        Task<Palestrante> GetByIdAsync(int palestranteId, bool includeEventos);
    }
}
