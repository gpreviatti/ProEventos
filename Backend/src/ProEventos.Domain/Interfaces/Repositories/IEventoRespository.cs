using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IEventoRespository : IBaseRepository<Evento>
    {
        Task<Evento[]> GetAllAsync();
        Task<Evento[]> GetAllPaginatedAsync(int skip, int take);
        Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
