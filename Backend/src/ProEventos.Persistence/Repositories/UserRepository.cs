using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly ProEventosContext _context;

        public UserRepository(ProEventosContext context) : base(context) { }

        public async Task<User> GetByIdAsync(int id) => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> GetByUserNameAsync(string name) => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == name);
    }
}
