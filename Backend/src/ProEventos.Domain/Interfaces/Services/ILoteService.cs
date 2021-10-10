using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface ILoteService
    {
        Task<LoteDto> Salvar(int eventoId, LoteDto lote);
        Task<bool> Deletar(int eventoId, int loteId);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}
