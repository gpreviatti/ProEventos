using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ProUsers.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        private readonly SymmetricSecurityKey _key;

        public AccountService(
            IConfiguration config,
            UserManager<User> userManager,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<UserDto> SalvarAsync(UserCreateDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                    return _mapper.Map<UserDto>(user);

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Erro ao tentar criar conta. Erro: {exception.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAsync(UserUpdateDto userDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userDto.Id);
                if (user == null) return null;

                var updatedUser = _mapper.Map(userDto, user);
                string token = "";
                token = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                if (userDto.Password != null)
                    await _userManager.ResetPasswordAsync(user, token, userDto.Password);

                if (await _userRepository.UpdateAsync(updatedUser)) {
                    userDto.Token = token;
                    return _mapper.Map(user, userDto);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> DeletarAsync(int UserId)
        {
            try
            {
                var User = await _userRepository.GetByIdAsync(UserId);
                if (User == null) throw new Exception("Usuario para remoção não encontrado");

                return await _userRepository.DeleteAsync(User);
            }
            catch (Exception exception)
            {

                throw new Exception($"Erro ao tentar deletar conta. Erro: {exception.Message}");
            }
        }

        public async Task<UserDto> GetByIdAsync(int UserId)
        {
            try
            {
                var User = await _userRepository.GetByIdAsync(UserId);
                if (User == null) return null;

                var resultado = _mapper.Map<UserDto>(User);

                return resultado;
            }
            catch (Exception exception)
            {
                throw new Exception($"Erro: {exception.Message}");
            }
        }

        public async Task<UserDto> GetByUserNameAsync(string name)
        {
            try
            {
                var user = await _userRepository.GetByUserNameAsync(name);
                if (user == null) return null;

                var resultado = _mapper.Map<UserDto>(user);

                return resultado;
            }
            catch (Exception exception)
            {

                throw new Exception($"Erro: {exception.Message}");
            }
        }

        public async Task<bool> CheckUserPasswordAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _userManager
                .Users
                .SingleOrDefaultAsync(u => u.UserName.ToLower() == userLoginDto.UserName.ToLower());

                return await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            }
            catch (Exception exception)
            {
                throw new Exception($"Erro ao tentar verificar senha. Erro: {exception.Message}");
            }
        }

        public async Task<string> CreateToken(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescription);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception exception)
            {
                throw new Exception($"Não foi possível gerar o token. Erro: {exception.Message}");
            }
        }
    }
}
