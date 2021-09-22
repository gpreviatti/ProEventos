using System.Collections.Generic;
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
    public class EventosController : ControllerBase
    {
        private readonly ILogger<EventosController> _logger;
        private readonly DataContext _context;

        public EventosController(
            ILogger<EventosController> logger,
            DataContext context
        )
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get all resources
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Evento>> Get() => await _context
            .Eventos
            .ToListAsync();

        /// <summary>
        /// Get resource by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Evento> GetById(int id) => await _context
            .Eventos
            .FirstOrDefaultAsync(x => x.EventoId == id);

        /// <summary>
        /// Add or update resource
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Evento> Post(Evento evento)
        {
            if (evento.EventoId > 0)
                _context.Eventos.Update(evento);
            else
                await _context.Eventos.AddAsync(evento);

            await _context.SaveChangesAsync();
            return evento;
        }

        /// <summary>
        /// Delete resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            var evento = await _context
                .Eventos
                .FirstOrDefaultAsync(x => x.EventoId == id);

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
