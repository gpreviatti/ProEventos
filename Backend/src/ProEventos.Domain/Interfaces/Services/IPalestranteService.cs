using ProEventos.Domain.Dtos;
using ProEventos.Domain.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface IPalestranteService : IBaseService<Palestrante>
    {
        Task<PalestranteDto[]> GetPalestrantesAsync();
        Task<PaginatedResponse<IEnumerable<PalestranteDto>>> GetPalestrantesPaginatedAsync(PaginatedRequest paginatedRequest);
    }
}
