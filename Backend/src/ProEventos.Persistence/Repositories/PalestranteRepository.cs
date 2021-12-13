using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class PalestranteRepository : BaseRepository<Palestrante>, IPalestranteRepository
    {
        public PalestranteRepository(ProEventosContext context) : base(context) { }

        public async Task<Palestrante[]> GetAllPaginatedAsync(
            int currentPage, 
            int pageSize, 
            string searchValue = ""
        )
        {
            var query = _context.Palestrantes.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(
                    e => e.Email.ToLower().Contains(searchValue.ToLower()) || 
                        e.Nome.ToLower().Contains(searchValue.ToLower()) 
                );

            return await query
                .OrderBy(e => e.Id)
                .Skip((currentPage-1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
