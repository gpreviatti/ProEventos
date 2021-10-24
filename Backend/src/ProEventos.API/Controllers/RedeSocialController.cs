using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedeSocialController : ControllerBase
    {
        private readonly IRedeSocialService _service;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RedeSocialController(
            IRedeSocialService service,
            IWebHostEnvironment hostEnvironment
        )
        {
            _hostEnvironment = hostEnvironment;
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
                var redeSocial = await _service.GetRedeSocialByIdAsync(id);

                if (redeSocial == null)
                    return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
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
                var redeSocial = await _service.DeletarAsync(id);

                if (!redeSocial)
                    return NoContent();

                return Ok(redeSocial);
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
