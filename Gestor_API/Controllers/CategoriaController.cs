using Gestor_API.Contracts;
using Gestor_API.Entities.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoriaController : ControllerBase
    {
        #region 
        private readonly ICategoriaRepository _categoriaRepo;

        public CategoriaController(ICategoriaRepository categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }
        #endregion


        [HttpGet("GetCategoria")]
        [Authorize]
        public async Task<IActionResult> GetCategoria()
        {
            //var user = User.Identity.Name;
            //string[] usuario = user.Split(';');
            //var id_usuario = Convert.ToInt32(usuario[1].ToString());

            var categoria = await _categoriaRepo.GetCategoria();
            if (categoria == null) return BadRequest(new { message = "Nehum categoria localizada." });

            return Ok(categoria);
        }

        [HttpGet("GetCategoriaNome")]
        //[Authorize]
        public async Task<IActionResult> GetCategoriaNome([FromQuery] string nome)
        {
            var ret = "";
            if (String.IsNullOrWhiteSpace(nome)) ret += "Favor informar uma categoria";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var categoria = await _categoriaRepo.GetCategoriaNome(nome);
            if (categoria == null) return BadRequest(new { message = "Nehuma categoria localizada." });

            return Ok(categoria);
        }

        [HttpGet("GetCategoriaId/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetCategoriaId(int Id)
        {
            var ret = "";
            if (Id == 0) ret += "Digite um Id valido";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var categoria = await _categoriaRepo.GetCategoriaId(Id);
            if (categoria == null) return BadRequest(new { message = "OPS!! Nenhuma categoria localizada" });

            return Ok(categoria);
        }

        //create
        [HttpPost("CreateCategoria")]
        [Authorize]
        public async Task<IActionResult> CreateCategoria(Categoria categoria)
        {
            //var user = User.Identity.Name;
            //string[] usuario = user.Split(';');
            //int cod = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            //if (categoria.Id == 0) ret += "Categoria não informado ";
            if (categoria.ds_descricao == null) ret += "Categoria não informada ";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var categ = await _categoriaRepo.CreateCategoria(categoria);
            if (categ == null) return BadRequest(new { desp = "ops!! não foi possivel salvar a nova categoria." });

            return Ok(categ);

        }

        //update
        [HttpPut("UpdateCategoria/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            //var user = User.Identity.Name;
            //string[] usuario = user.Split(';');
            //int cod = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            if (categoria.Id == 0) ret += "Categoria não informado ";
            if (categoria.ds_descricao == null) ret += "Categoria não informada ";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            if (id == categoria.Id)
            {
                var categ = await _categoriaRepo.UpdateCategoria(categoria);
                if (categ == null) return BadRequest(new { desp = "ops!! não foi possivel alterar a nova categoria." });
                return Ok(categ);
            }
            else
            {
                return BadRequest(new { desp = "OPS!! Informações estao diferentes!" });
            }
        }

        //delete
        [HttpDelete("DeleteCategoria/{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            //var user = User.Identity.Name;
            //string[] usuario = user.Split(';');
            //int cod = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            if (id == 0) ret += "ID da categoria não foi informado ";
            
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });
            
            var categoria = await _categoriaRepo.GetCategoriaId(id);

            if(categoria != null)
            {
                await _categoriaRepo.DeletarCategoria(categoria);
                return Ok("excluido com sucesso");
            }
            else
            {
                return NotFound($"Não foi possivel excluir o ID = {id}!!");
            }
        }
    }
}
