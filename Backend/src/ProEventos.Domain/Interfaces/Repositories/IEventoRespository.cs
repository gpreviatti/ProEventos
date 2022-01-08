using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IEventoRespository : IBaseRepository<Evento>
    {
        Task<Evento[]> GetAllPaginatedAsync(int currentPage, int pageSize, string searchValue = "");
        Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
