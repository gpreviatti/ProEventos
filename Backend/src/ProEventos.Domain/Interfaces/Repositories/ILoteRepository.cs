using System.Threading.Tasks;

namespace ProEventos.Domain.Interfaces
{
    public interface ILoteRepository : IBaseRepository<Lote>
    {
        /// <summary>
        /// Método get que retornará uma lista de lotes por eventoId. 
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <returns>Array de Lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Método get que retornará apenas 1 Lote
        /// </summary>
        /// <param name="id">Código chave da tabela Lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetByIdsAsync(int id);
    }
}
