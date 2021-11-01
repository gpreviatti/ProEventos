using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRespository
    {
        public EventoRepository(ProEventosContext context) : base(context) { }

        public async Task<Evento[]> GetAllAsync()
        {
            return await _context
                .Eventos
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .ToArrayAsync();
        }

        public async Task<Evento[]> GetAllPaginatedAsync(int skip, int take)
        {
            return await _context
                .Eventos
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
