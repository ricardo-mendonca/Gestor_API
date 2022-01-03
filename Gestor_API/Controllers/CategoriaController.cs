using Gestor_API.Contracts;
using Gestor_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    [Route("v1/Categoria")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepo;

        public CategoriaController(ICategoriaRepository categoriaRepo)
        {
			_categoriaRepo = categoriaRepo;
        }


		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetCategoria()
		{
			try
			{
				var categoria = await _categoriaRepo.GetCategoria();
				return Ok(categoria);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("/{nome}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetCategoriaNome(string nome)
		{
			try
			{
				var categoria = await _categoriaRepo.GetCategoriaNome(nome);
				return Ok(categoria);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetCategoriaId(int Id)
		{
			try
			{
				var categoria = await _categoriaRepo.GetCategoriaId(Id);
				if (categoria == null)
					return BadRequest(new { message = "Id inválidos" });

				return Ok(categoria);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

	}
}
