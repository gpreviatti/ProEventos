using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRespository
    {
        public EventoRepository(ProEventosContext context) : base(context) { }

        public async Task<Evento[]> Get(int userId) => await _context
            .Set<Evento>()
            .Where(e => e.UserId == userId)
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .ToArrayAsync();

        public async Task<Evento[]> GetAllPaginatedAsync(
            int userId,
            int currentPage, 
            int pageSize, 
            string searchValue = ""
        )
        {
            var query = _context.Eventos.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(
                    e => e.Tema.ToLower().Contains(searchValue.ToLower()) || 
                        e.Local.ToLower().Contains(searchValue.ToLower()) 
                );

            return await query
                .OrderBy(e => e.Id)
                .Where(e => e.UserId == userId)
                .Skip((currentPage-1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Evento[]> GetAllByTemaAsync(int userId, string tema, bool includePalestrantes = false)
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
                .Where(e => 
                    e.Tema.ToLower().Contains(tema.ToLower()) && 
                    e.UserId == userId
                );

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
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
                         .Where(e => e.Id == eventoId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
