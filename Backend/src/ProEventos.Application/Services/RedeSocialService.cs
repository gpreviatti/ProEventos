using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;

namespace ProEventos.Application
{
    public class RedeSocialService : BaseService<RedeSocial>, IRedeSocialService
    {
        private readonly IRedeSocialRepository _redeSocialRepository;

        public RedeSocialService(
            IRedeSocialRepository redeSocialRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _redeSocialRepository = redeSocialRepository;
        }

        public async Task<RedeSocialDto[]> GetRedesSociaisByPalestranteIdAsync(int palestranteId)
        {
            var redeSociais = await _redeSocialRepository
                .GetRedesSociaisByPalestranteIdAsync(palestranteId);

            var redeSociaisDto = _mapper.Map<IEnumerable<RedeSocialDto>>(redeSociais);

            return redeSociaisDto.ToArray();
        }

        public async Task<RedeSocialDto> GetRedeSocialByIdAsync(int redeSocialId)
        {
            var redeSocial = await _redeSocialRepository
                .GetByIdAsync(redeSocialId);

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<RedeSocialDto> SalvarAsync(RedeSocialDto redeSocialDto)
        {
            var redeSocial = new RedeSocial();
            if (redeSocialDto.Id == 0)
            {
                redeSocial = _mapper.Map<RedeSocial>(redeSocialDto);

                await _redeSocialRepository.AddAsync(redeSocial);
            }
            else
            {
                redeSocial = await _redeSocialRepository.GetByIdAsync(redeSocialDto.Id);
                if (redeSocial == null) return null;

                redeSocial = _mapper.Map<RedeSocial>(redeSocialDto);

                await _redeSocialRepository.UpdateAsync(redeSocial);
            }

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var redeSocial = await _redeSocialRepository.GetByIdAsync(id);

            if (redeSocial == null) 
                throw new Exception("Rede social não encontrada.");

            return await _redeSocialRepository.DeleteAsync(redeSocial);
        }
    }
}
