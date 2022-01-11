using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;

namespace ProUsers.Application
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public AccountService(
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> SalvarAsync(UserDto UserDto)
        {
            User User;

            if (UserDto.Id == 0)
            {
                User = _mapper.Map<User>(UserDto);

                await _userRepository.AddAsync(User);
            }
            else
            {
                User = await _userRepository.GetByIdAsync(UserDto.Id);
                if (User == null) 
                    return null;

                User = _mapper.Map<User>(UserDto);

                await _userRepository.UpdateAsync(User);
            }

            return _mapper.Map<UserDto>(User);
        }

        public async Task<bool> DeletarAsync(int UserId)
        {
            var User = await _userRepository.GetByIdAsync(UserId);
            if (User == null) throw new Exception("User para delete não encontrado.");

            return await _userRepository.DeleteAsync(User);
        }

        public async Task<UserDto> GetByIdAsync(int UserId)
        {
            var User = await _userRepository.GetByIdAsync(UserId);
            if (User == null) return null;

            var resultado = _mapper.Map<UserDto>(User);

            return resultado;
        }

        public async Task<bool> UserExists(string name)
        {
            var user = await _userRepository.GetByNameAsync(name);

            return user != null;
        }
        public async Task<UserDto> GetByNameAsync(string name)
        {
            var User = await _userRepository.GetByNameAsync(name);
            if (User == null) return null;

            var resultado = _mapper.Map<UserDto>(User);

            return resultado;
        }

        // private async Task<SignInResult> CheckUserPasswordAsync(UserDto user)
        // {

        // }
    }
}
