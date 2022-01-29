using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class PalestranteRepository : BaseRepository<Palestrante>, IPalestranteRepository
    {
        public PalestranteRepository(ProEventosContext context) : base(context) { }

        public async Task<Palestrante[]> Get(int userId) => await _context
            .Palestrantes
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .OrderBy(e => e.Id)
            .ToArrayAsync();

        public async Task<Palestrante[]> GetAllPaginatedAsync(
            int userId,
            int currentPage, 
            int pageSize, 
            string searchValue = ""
        )
        {
            var query = _context.Palestrantes.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(
                    e => e.User.Email.ToLower().Contains(searchValue.ToLower()) ||
                         e.User.UserName.ToLower().Contains(searchValue.ToLower()) ||
                         e.User.LastName.ToLower().Contains(searchValue.ToLower())
                );
            }

            return await query
                .Where(p => p.UserId == userId)
                .OrderBy(e => e.Id)
                .Skip((currentPage-1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Palestrante> GetByIdAsync(int userId, int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Id == palestranteId && p.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
