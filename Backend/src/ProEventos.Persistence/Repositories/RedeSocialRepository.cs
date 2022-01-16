using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class RedeSocialRepository : BaseRepository<RedeSocial>, IRedeSocialRepository
    {
        public RedeSocialRepository(ProEventosContext context) : base(context) { }

        public async Task<RedeSocial[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId) 
        {
            var palestrante = await _context
                .Palestrantes
                .FirstOrDefaultAsync(p => p.Id == palestranteId);

            if (palestrante is null)
                throw new InvalidOperationException("Palestrante não encontrado");

            return await _context.RedesSociais.Where(r => r.PalestranteId == palestranteId).ToArrayAsync();
        }

        public async Task<RedeSocial> GetByIdAsync(int id) => await _context
            .RedesSociais
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
