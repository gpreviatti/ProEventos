using ProEventos.Domain.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface IPalestranteService
    {
        Task<PalestranteDto[]> GetPalestrantesAsync();
        Task<PaginatedResponse<IEnumerable<PalestranteDto>>> GetPalestrantesPaginatedAsync(PaginatedRequest paginatedRequest);
        Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId);
        Task<PalestranteDto> SalvarAsync(PalestranteDto palestrante);
        Task<bool> DeletarAsync(int palestranteId);
    }
}
