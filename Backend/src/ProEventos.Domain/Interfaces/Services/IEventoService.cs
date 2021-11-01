using System.Threading.Tasks;
using ProEventos.Domain.Messages;

namespace ProEventos.Domain
{
    public interface IEventoService
    {
        Task<EventoDto> SalvarAsync(EventoDto model);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetAllEventosAsync();
        Task<PaginatedListResponse<EventoDto>> GetAllEventosPaginatedAsync(int skip, int take);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
