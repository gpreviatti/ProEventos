using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;
using ProEventos.Domain.Messages;

namespace ProEventos.Application
{
    public class PalestranteService : BaseService<Palestrante>, IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteService(
            IPalestranteRepository palestranteRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _palestranteRepository = palestranteRepository;
        }

        public async Task<PalestranteDto[]> GetPalestrantesAsync() 
        {
            var palestrantes = await _palestranteRepository.Get();

            var palestrantesDto = _mapper.Map<IEnumerable<PalestranteDto>>(palestrantes);

            return palestrantesDto.ToArray();
        }

        public async Task<PaginatedResponse<IEnumerable<PalestranteDto>>> GetPalestrantesPaginatedAsync(PaginatedRequest paginatedRequest)
        {
            var data = await _palestranteRepository.GetAllPaginatedAsync(
                paginatedRequest.CurrentPage,
                paginatedRequest.PageSize,
                paginatedRequest.SearchValue
            );
            if (data == null) return null;

            var total = await _palestranteRepository.GetAllCountAsync();

            var dataMapped = _mapper.Map<PalestranteDto[]>(data);

            return new PaginatedResponse<IEnumerable<PalestranteDto>>(
                dataMapped,
                paginatedRequest.CurrentPage,
                paginatedRequest.PageSize,
                total,
                dataMapped.Count(),
                paginatedRequest.SearchValue
            ); ;
        }

        public async Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId) 
        {
            var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(palestranteId, false);

            return _mapper.Map<PalestranteDto>(palestrante);
        }

        public async Task<PalestranteDto> SalvarAsync(PalestranteDto palestranteDto)
        {
            var palestrante = new Palestrante();

            if (palestranteDto.Id == 0)
            {
                palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.AddAsync(palestrante);
            }
            else
            {
                palestrante = await _palestranteRepository.GetByIdAsync(palestranteDto.Id);
                if (palestrante == null) 
                    return null;

                palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.UpdateAsync(palestrante);
            }

            return _mapper.Map<PalestranteDto>(palestrante);
        }

        public async Task<bool> DeletarAsync(int palestranteId)
        {
            var palestrante = await _palestranteRepository.GetByIdAsync(palestranteId);
            if (palestrante == null) 
                throw new Exception("Palestrante para encontrado.");

            return await _palestranteRepository.DeleteAsync(palestrante);
        }
    }
}
