using AutoMapper;
using ProEventos.Domain;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        protected readonly IBaseRepository<T> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(
            IBaseRepository<T> baseRepository,
            IMapper mapper
        ) 
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<D> GetByIdAsync<D>(int id) where D : EntityDto
        {
            var palestrante = await _baseRepository.GetByIdAsync(id);

            return _mapper.Map<D>(palestrante);
        }

        public async Task<D> SalvarAsync<D>(D dto) where D : EntityDto
        {
            T entity;
            if (dto.Id == 0)
            {
                entity = _mapper.Map<T>(dto);

                await _baseRepository.AddAsync(entity);
            }
            else
            {
                entity = await _baseRepository.GetByIdAsync(dto.Id);
                if (entity == null)
                    return null;

                entity = _mapper.Map<T>(dto);

                await _baseRepository.UpdateAsync(entity);
            }

            return _mapper.Map<D>(entity);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var lote = await _baseRepository.GetByIdAsync(id);
            if (lote == null) throw new Exception("Recurso não encontrado para remoção");

            return await _baseRepository.DeleteAsync(lote);
        }
    }
}
