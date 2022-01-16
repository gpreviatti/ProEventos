using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IEventoRespository : IBaseRepository<Evento>
    {
        Task<Evento[]> Get(int userId);
        Task<int> GetAllCountAsync();
        Task<Evento[]> GetAllByTemaAsync(int userId, string tema, bool includePalestrantes = false);
        Task<Evento> GetByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
        Task<Evento[]> GetAllPaginatedAsync(int userId, int currentPage, int pageSize, string searchValue = "");
    }
}
