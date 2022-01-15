using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ProEventosContext _context;

        public UserRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User entity) 
        {
            await _context.AddAsync(entity);

            return await SaveChangesAsync();
        } 

        public async Task<bool> UpdateAsync(User entity) 
        {
            await Task.Run(() => _context.Update(entity));

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(User entity) 
        {
            await Task.Run(() =>_context.Remove(entity));

            return await SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User> GetByUserNameAsync(string name) => await _context
            .Users
            .FirstOrDefaultAsync(u => u.UserName == name);

        private async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
