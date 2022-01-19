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

            var usuarios = await _usuarioRepo.Getusuarios();
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);

        }

        [HttpGet("/pesquisar/{nome}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsuarioNome(string nome)
        {
            var ret = "";
            if (String.IsNullOrWhiteSpace(nome)) ret += "Favor informar um nome";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var usuarios = await _usuarioRepo.GetUsuarioNome(nome);
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsuarioId(int Id)
        {
            var ret = "";
            if (Id == 0) ret += "Digite um Id valido";
            if (ret != null) return BadRequest(new { message = ret });

            var usuario = await _usuarioRepo.GetUsuarioId(Id);
            if (usuario == null) return BadRequest(new { message = "Nenhum usuario Localizado" });

            return Ok(usuario);
        }


        

        [HttpPost("/cadastrar")]
        public async Task<IActionResult> CreateUsuario(Usuario usuario)
        {
            var ret = "";
            if (usuario.cd_cpf == 0) ret += "CPF Invalido; ";
            if (String.IsNullOrWhiteSpace(usuario.ds_nome)) ret += " Favor informar um nome;";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var user = await _usuarioRepo.CreateUsuario(usuario);
            return Ok(user);
        }
    }
}
