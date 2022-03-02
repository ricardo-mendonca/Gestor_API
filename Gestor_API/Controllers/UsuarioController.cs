using Gestor_API.Contracts;
using Gestor_API.Entities;
using Gestor_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Gestor_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] Usuario usuario)
        {
            //recupera usuario
            var user = await _usuarioRepo.login(usuario.ds_email, usuario.ds_senha);
 
            // verifica se o usuario existe
            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalido" });

            //gera o token
            var token = TokenService.GenerateToken(user);

            //Ocultar senha
            user.ds_senha = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("CreateUsuario")]
        public async Task<ActionResult<dynamic>> CreateUsuario([FromBody] Usuario usuario)
        {
            var ret = "";
            if (usuario.cd_cpf == 0) ret += "CPF Invalido; ";
            if (String.IsNullOrWhiteSpace(usuario.ds_nome)) ret += " Favor informar um nome;";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var user = await _usuarioRepo.CreateUsuario(usuario);
            return Ok(user);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult<dynamic>> ResetPassword([FromBody] Usuario usuario)
        {
            var ret = "";
            if (String.IsNullOrWhiteSpace(usuario.ds_email)) ret += " Favor informar um E-mail valido. ";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var user = await _usuarioRepo.ResetPassword(usuario);
            if (user.ds_nome != null) return BadRequest(new { message = "Nenhum usuario Localizado" });


            return Ok(new { message = "Sua nova senha é 654321" });
        }


        [HttpGet("getUsuario")]
        [Authorize]
        public async Task<IActionResult> getUsuario()
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            int Id_usuario = Convert.ToInt32(usuario[1].ToString());


            var usuarios = await _usuarioRepo.GetUsuarioId(Id_usuario);
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);

        }

        [HttpPut("updateUsuario")]
        [Authorize]
        public async Task<ActionResult<dynamic>> updateUsuario([FromBody] Usuario usuario)
        {
            var user = User.Identity.Name;
            string[] v = user.Split(';');
            int Id_usuario = Convert.ToInt32(v[1].ToString());
            usuario.Id = Id_usuario;

            var ret = "";
            if (usuario.Id == 0) ret += "Id Invalido; ";
            if (String.IsNullOrWhiteSpace(usuario.ds_nome)) ret += " Favor informar um nome;";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var usuarios = await _usuarioRepo.updateUsuario(usuario);
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);

        }





        [HttpGet("pesquisarPorNome")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioNome(string nome)
        {
            var ret = "";
            if (String.IsNullOrWhiteSpace(nome)) ret += "Favor informar um nome";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var usuarios = await _usuarioRepo.GetUsuarioNome(nome);
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);
        }

        [HttpGet("pesquisarPorId")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioId(int Id)
        {
            var ret = "";
            if (Id == 0) ret += "Digite um Id valido";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var usuario = await _usuarioRepo.GetUsuarioId(Id);
            if (usuario == null) return BadRequest(new { message = "Nenhum usuario Localizado" });

            return Ok(usuario);
        }

       
    }
}
