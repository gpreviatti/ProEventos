using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Messages;

namespace ProEventos.Domain.Interfaces
{
    public interface IEventoService : IBaseService<Evento>
    {
        Task<EventoDto[]> GetAllEventosAsync(int userId);
        Task<PaginatedResponse<IEnumerable<EventoDto>>> GetAllEventosPaginatedAsync(int userId, PaginatedRequest paginatedRequest);
        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false);
        Task<bool> DeletarAsync(int userId, int id);
    }
}
