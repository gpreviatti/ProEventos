﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Domain.Messages;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly string resourcesPath = @"Resources/Images/Palestrantes/";

        public PalestranteController(
            IPalestranteService palestranteService,
            IWebHostEnvironment hostEnvironment
        )
        {
            _hostEnvironment = hostEnvironment;
            _palestranteService = palestranteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var palestrantes = await _palestranteService.GetPalestrantesAsync();

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

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedAsync([FromQuery] PaginatedRequest paginatedRequest)
        {
            try
            {
                var eventos = await _palestranteService.GetPalestrantesPaginatedAsync(paginatedRequest);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByIdAsync(id);

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
                var palestrante = await _palestranteService.SalvarAsync(palestranteDto);
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
                var redeSocial = await _palestranteService.DeletarAsync(id);

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

        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage(int id)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByIdAsync(id);
                if (palestrante == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(palestrante.ImagemURL);
                    palestrante.ImagemURL = await SaveImage(file);
                }
                var EventoRetorno = await _palestranteService.SalvarAsync(palestrante);

                return Ok(EventoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar imagem ao palestrante. Erro: {ex.Message}"
                );
            }
        }

        #region Private Methods
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(
                Path
                .GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10)
                .ToArray()
            ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, resourcesPath, imageName);

            using var fileStream = new FileStream(imagePath, FileMode.Create);
            
            await imageFile.CopyToAsync(fileStream);

            return imageName;
        }

        private void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, resourcesPath, imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
        #endregion
    }
}
