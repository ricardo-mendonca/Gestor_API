using Gestor_API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepo;

        public CategoriaController(ICategoriaRepository categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }



        [HttpGet("GetCategoria")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoria()
        {
            var categoria = await _categoriaRepo.GetCategoria();
            if (categoria == null) return BadRequest(new { message = "Nehum categoria localizada." });

            return Ok(categoria);
        }

        [HttpGet("GetCategoriaNome/{nome}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriaNome(string nome)
        {
            var ret = "";
            if (String.IsNullOrWhiteSpace(nome)) ret += "Favor informar uma categoria";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var categoria = await _categoriaRepo.GetCategoriaNome(nome);
            if (categoria == null) return BadRequest(new { message = "Nehuma categoria localizada." });

            return Ok(categoria);
        }

        [HttpGet("GetCategoriaId/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriaId(int Id)
        {
            var ret = "";
            if (Id == 0) ret += "Digite um Id valido";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var categoria = await _categoriaRepo.GetCategoriaId(Id);
            if (categoria == null) return BadRequest(new { message = "Id inválidos" });

            return Ok(categoria);
        }

    }
}
