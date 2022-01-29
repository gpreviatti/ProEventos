using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> Get(int userId);
        Task<Palestrante[]> GetAllPaginatedAsync(
            int userId, int currentPage, int pageSize, string searchValue = ""
        );
        Task<Palestrante> GetByIdAsync(
            int userId, int palestranteId, bool includeEventos
        );
    }
}
