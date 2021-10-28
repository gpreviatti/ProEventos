using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public interface ILoteService
    {
        Task<LoteDto> Salvar(LoteDto lote);
        Task<bool> Deletar(int loteId);
        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int loteId);
    }
}
