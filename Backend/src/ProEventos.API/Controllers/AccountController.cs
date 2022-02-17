using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetUserByNameAsync")]
        public async Task<IActionResult> GetUserAsync()
        {
            return Ok(await _accountService.GetByUserNameAsync(User.GetUserName()));
        }

        [AllowAnonymous]
        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync(UserCreateDto userCreateDto)
        {
            if (await _accountService.GetByUserNameAsync(userCreateDto.UserName) != null)
                return BadRequest("O usuario já existe");

            var user = await _accountService.SalvarAsync(userCreateDto);
            if (user != null)
                return Created("", user);

            return BadRequest("Erro ao cadastrar usuário. Tente mais tarde!");
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            userUpdateDto.Id = User.GetUserId();
            var user = await _accountService.UpdateAsync(userUpdateDto);
            if (user != null)
                return Ok(user);

            return BadRequest("Erro ao atualizar usuário. Tente mais tarde!");
        }

        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync()
        {
            if (await _accountService.DeletarAsync(User.GetUserId()) == true)
                return Ok("Usuário removido com sucesso!");

            return BadRequest("Erro ao remover usuário. Tente mais tarde!");
        }

        [AllowAnonymous]
        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _accountService.GetByUserNameAsync(userLoginDto.UserName);
            if (user == null) 
                return Unauthorized("Usuário não existe");

            // if (await _accountService.CheckUserPasswordAsync(userLoginDto) == false)  
            //     return Unauthorized("senha está incorreta");

            return Ok(new UserLoginResultDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                Token = await _accountService.CreateToken(user)
            });
        }
    }
}
