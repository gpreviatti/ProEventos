using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEnumerable<Evento> _eventos = new Evento[]
        {
            new Evento
            {
                EventoId = 1,
                Tema = "Angular 11 e .Net 5",
                Local = "Belo Horizonte",
                Lote = "1° Lote",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImagemUrl = "foto.png"
            },
            new Evento
            {
                EventoId = 2,
                Tema = "Angular e suas novidas",
                Local = "São Paulo",
                Lote = "2° Lote",
                QtdPessoas = 350,
                DataEvento = DateTime.Now.AddDays(20).ToString(),
                ImagemUrl = "foto.png"
            },
        };
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
        public IEnumerable<Evento> Get() => _context.Eventos;

        [HttpGet("{id}")]
        public Evento GetById(int id) => _context.Eventos.FirstOrDefault(x => x.EventoId == id);

        [HttpPost]
        public Evento Post(Evento evento)
        {
            _context.Eventos.Add(evento);
            return evento;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var evento = _context.Eventos.Where(x => x.EventoId == id).FirstOrDefault();
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                return true;
            }
            return false;
        }
    }
}
