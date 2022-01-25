using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProEventosContext _context;
        public UserRepository(ProEventosContext context) 
        {
            _context = context;
        }

        public async Task<User> GetByUserNameAsync(string name) => 
            await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == name);

        public async Task<User> GetByIdAsync(int id) =>
            await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<bool> AddAsync(User entity)
        {
            _context.Add(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            _context.Update(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            _context.Remove(entity);

            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync() => 
            (await _context.SaveChangesAsync()) > 0;
    }
}
