using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface ILoteService : IBaseService<Lote>
    {
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
    }
}
