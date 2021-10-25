using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;

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
                .GetRedeSocialByIdAsync(redeSocialId);

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<RedeSocialDto> SalvarAsync(RedeSocialDto redeSocialDto)
        {
            var redeSocial = new RedeSocial();
            if (redeSocialDto.Id == 0)
            {
                redeSocial = _mapper.Map<RedeSocial>(redeSocialDto);

                await _redeSocialRepository.Add(redeSocial);
            }
            else
            {
                redeSocial = await _redeSocialRepository.GetById(redeSocialDto.Id);
                if (redeSocial == null) return null;

                redeSocial = _mapper.Map<RedeSocial>(redeSocialDto);

                await _redeSocialRepository.Update(redeSocial);
            }

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var redeSocial = await _redeSocialRepository.GetById(id);

            if (redeSocial == null) 
                throw new Exception("Rede social não encontrada.");

            return await _redeSocialRepository.Delete(redeSocial);
        }
    }
}