using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Dtos;
using ProEventos.Domain.Interfaces;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LotesController(ILoteService LoteService)
        {
            _loteService = LoteService;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar lotes. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLotes(LoteDto lote)
        {
            try
            {
                var lotes = await _loteService.Salvar(lote);
                if (lotes == null) return NoContent();

                return Created("",lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar lotes. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lote = await _loteService.GetLoteByIdsAsync(id);
                if (lote == null) return NoContent();

                return await _loteService.Deletar(id) 
                       ? Ok(new { message = "Lote Deletado" }) 
                       : throw new Exception("Ocorreu um problem não específico ao tentar deletar Lote.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar lotes. Erro: {ex.Message}"
                );
            }
        }
    }
}
