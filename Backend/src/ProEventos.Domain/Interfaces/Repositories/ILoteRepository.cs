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
        Task<Lote[]> GetByEventoIdAsync(int eventoId);
    }
}
