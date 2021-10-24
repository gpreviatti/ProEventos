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
    public class PalestranteController : ControllerBase
    {
        private readonly IPalestranteService _service;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PalestranteController(
            IPalestranteService service,
            IWebHostEnvironment hostEnvironment
        )
        {
            _hostEnvironment = hostEnvironment;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var palestrantes = await _service.GetPalestrantesAsync();

                if (palestrantes == null)
                    return NoContent();

                return Ok(palestrantes);
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
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var palestrante = await _service.GetPalestranteByIdAsync(id);

                if (palestrante == null)
                    return NoContent();

                return Ok(palestrante);
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
        public async Task<IActionResult> PostAsync(PalestranteDto palestranteDto)
        {
            try
            {
                var palestrante = await _service.SalvarAsync(palestranteDto);
                if (palestrante == null) return NoContent();

                return Created("", palestrante);
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
