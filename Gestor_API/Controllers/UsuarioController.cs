using Gestor_API.Contracts;
using Gestor_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Gestor_API.Controllers
{
    [Route("v1/Usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Getusuario()
		{
			try
			{
				var usuarios = await _usuarioRepo.Getusuarios();
				return Ok(usuarios);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetUsuarioId(int Id)
		{
			try
			{

				var usuario = await _usuarioRepo.GetUsuarioId(Id);
				if (usuario == null)
					return NotFound();

				return Ok(usuario);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}


		[HttpGet("/pesquisar/{nome}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetUsuarioNome(string nome)
		{
			try
			{
				var usuarios = await _usuarioRepo.GetUsuarioNome(nome);
				return Ok(usuarios);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("/cadastrar")]
		public async Task<IActionResult> CreateUsuario(Usuario usuario)
		{
			try
			{
                if (usuario.ds_nome == null )
                {
					return BadRequest(new { message = "Usuário ou senha inválidos" });
				}
				var user = await _usuarioRepo.CreateUsuario(usuario);
				return Ok(user);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}
	}
}
