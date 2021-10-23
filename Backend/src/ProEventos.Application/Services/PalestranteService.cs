using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

namespace ProEventos.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IMapper _mapper;

        public PalestranteService(
            IPalestranteRepository palestranteRepository,
            IMapper mapper
        )
        {
            _palestranteRepository = palestranteRepository;
            _mapper = mapper;
        }

        public async Task<PalestranteDto[]> GetPalestrantesAsync() 
        {
            var palestrantes = await _palestranteRepository.GetAllPalestrantesAsync(false);

            var palestrantesDto = _mapper.Map<IEnumerable<PalestranteDto>>(palestrantes);

            return palestrantesDto.ToArray();
        }

        public async Task<PalestranteDto> GetPalestranteByIdAsync(int palestranteId) 
        {
            var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(palestranteId, false);

            return _mapper.Map<PalestranteDto>(palestrante);
        }

        public async Task<PalestranteDto> Salvar(PalestranteDto palestranteDto)
        {
            if (palestranteDto.Id == 0)
            {
                var palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.Add(palestrante);
            }
            else
            {
                var palestrante = await _palestranteRepository.GetById(palestranteDto.Id);
                if (palestrante == null) return null;

                palestrante = _mapper.Map<Palestrante>(palestranteDto);

                await _palestranteRepository.Update(palestrante);
            }

            return palestranteDto;
        }

        public async Task<bool> Deletar(int palestranteId)
        {
            var palestrante = await _palestranteRepository.GetById(palestranteId);
            if (palestrante == null) throw new Exception("Palestrante para não encontrado.");

            return await _palestranteRepository.Delete(palestrante);
        }
    }
}
