using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Gestor_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepo;
        
        public ClienteController(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        [HttpPost("CreateCliente")]
        [Authorize]
        public async Task<IActionResult> CreateCliente(Cliente cliente)
        {
            var user = User.Identity.Name;
            string[] v = user.Split(';');
            int Id_usuario = Convert.ToInt32(v[1].ToString());
            cliente.id_usuario = Id_usuario;

            var ret = "";
            if (cliente.ds_cpf_cnpj == null) ret += "CPF Invalido; ";
            if (String.IsNullOrWhiteSpace(cliente.ds_nome)) ret += " Favor informar um nome;";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });
            
            var cli = await _clienteRepo.CreateCliente(cliente);
            if (cli == null) return BadRequest(new { desp = "ops!! não foi ralizado o cadastro." });

            return Ok(cli);
        }

        


        //AlterCliente
        [HttpPost("AlterCliente")]
        [Authorize]
        public async Task<IActionResult> AlterCliente(Cliente cliente)
        {

            return Ok("");
        }


        //CancelCliente
        [HttpPost("CancelCliente")]
        [Authorize]
        public async Task<IActionResult> CancelCliente(Cliente cliente)
        {
            return Ok("");
        }





    }
}
