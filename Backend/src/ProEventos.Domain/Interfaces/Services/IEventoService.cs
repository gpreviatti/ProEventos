using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Messages;

namespace ProEventos.Domain.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> SalvarAsync(EventoDto model);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetAllEventosAsync();
        Task<PaginatedResponse<IEnumerable<EventoDto>>> GetAllEventosPaginatedAsync(PaginatedRequest paginatedRequest);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}
