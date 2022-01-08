using System.Threading.Tasks;
using ProEventos.Domain.Dtos;

namespace ProEventos.Domain.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDto> Salvar(LoteDto lote);
        Task<bool> Deletar(int loteId);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int loteId);
    }
}
