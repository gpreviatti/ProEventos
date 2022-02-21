using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RedeSocialController : ControllerBase
    {
        private readonly IRedeSocialService _service;

        public RedeSocialController(
            IRedeSocialService service
        )
        {
            _service = service;
        }

        [HttpGet("palestrante/{palestranteId}")]
        public async Task<IActionResult> GetRedesSociaisByPalestranteIdAsync(
            [FromRoute] int palestranteId
        )
        {
            try
            {
                var redeSocials = await _service.GetRedesSociaisByPalestranteIdAsync(palestranteId);

                if (redeSocials == null)
                    return NoContent();

                return Ok(redeSocials);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar recurso. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var redeSocial = await _service.GetByIdAsync<RedeSocialDto>(id);

                if (redeSocial == null)
                    return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar recurso. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(RedeSocialDto redeSocialDto)
        {
            try
            {
                var redeSocial = await _service.SalvarAsync(redeSocialDto);
                if (redeSocial == null) return NoContent();

                return Created("", redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar recurso. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (!await _service.DeletarAsync(id))
                    return NoContent();

                return Ok("RedeSocial removido com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar recurso. Erro: {ex.Message}"
                );
            }
        }
    }
}
