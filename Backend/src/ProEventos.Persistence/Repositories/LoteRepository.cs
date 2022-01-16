using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class LoteRepository : BaseRepository<Lote>, ILoteRepository
    {
        public LoteRepository(ProEventosContext context) : base(context) { }

        public async Task<Lote> GetByIdAsync(int id)
        {
            IQueryable<Lote> query = _context.Lotes;

            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync(lote => lote.Id == id);
        }

        public async Task<Lote[]> GetByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query
                .AsNoTracking()
                .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }
    }
}
