using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        private readonly DataContext _context;

        public EventoController(
            ILogger<EventoController> logger,
            DataContext context
        )
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Evento>> Get() => await _context
            .Eventos
            .ToListAsync();

        [HttpGet("{id}")]
        public async Task<Evento> GetById(int id) => await _context
            .Eventos
            .FirstOrDefaultAsync(x => x.EventoId == id);

        [HttpPost]
        public async Task<Evento> Post(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            var evento = await _context
                .Eventos
                .Where(x => x.EventoId == id)
                .FirstOrDefaultAsync();

            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
