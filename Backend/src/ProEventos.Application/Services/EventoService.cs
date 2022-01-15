using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;
using ProEventos.Domain.Messages;

namespace ProEventos.Application
{
    public class EventoService : BaseService<Evento>, IEventoService
    {
        private readonly IEventoRespository _eventoRepository;
        public EventoService(
            IEventoRespository eventoRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<EventoDto> SalvarAsync(int userId, EventoDto eventoDto)
        {
            Evento evento;
            eventoDto.UserId = userId;

            if (eventoDto.Id == 0)
            {
                evento = _mapper.Map<Evento>(eventoDto);

                await _eventoRepository.AddAsync(evento);
            }
            else
            {
                evento = await _eventoRepository.GetByIdAsync(eventoDto.Id);
                if (evento == null) 
                    return null;

                evento = _mapper.Map<Evento>(eventoDto);

                await _eventoRepository.UpdateAsync(evento);
            }

            return _mapper.Map<EventoDto>(evento);
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            var evento = await _eventoRepository.GetByIdAsync(userId, eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");

            return await _eventoRepository.DeleteAsync(evento);
        }

        public async Task<EventoDto[]> GetAllEventosAsync(int userId)
        {
            var eventos = await _eventoRepository.Get(userId);
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<PaginatedResponse<IEnumerable<EventoDto>>> GetAllEventosPaginatedAsync(int userId, PaginatedRequest paginatedRequest)
        {
            var data = await _eventoRepository.GetAllPaginatedAsync(
                userId,
                paginatedRequest.CurrentPage,
                paginatedRequest.PageSize,
                paginatedRequest.SearchValue
            );
            if (data == null) return null;

            var total = await _eventoRepository.GetAllCountAsync();

            var dataMapped = _mapper.Map<EventoDto[]>(data);

            return new PaginatedResponse<IEnumerable<EventoDto>>(
                dataMapped,
                paginatedRequest.CurrentPage,
                paginatedRequest.PageSize,
                total,
                dataMapped.Length,
                paginatedRequest.SearchValue
            ); ;
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            var eventos = await _eventoRepository.GetAllByTemaAsync(userId, tema, includePalestrantes);
            if (eventos == null) return null;

            var resultado = _mapper.Map<EventoDto[]>(eventos);

            return resultado;
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            var evento = await _eventoRepository.GetByIdAsync(userId, eventoId, includePalestrantes);
            if (evento == null) return null;

            var resultado = _mapper.Map<EventoDto>(evento);

            return resultado;
        }
    }
}
