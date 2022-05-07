using Gestor_API.Contracts;
using Gestor_API.Entities.Contas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaRepository _despesaRepo;

        public DespesaController(IDespesaRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;
        }



        [HttpPost("CreateDespesa")]
        [Authorize]
        public async Task<IActionResult> CreateDespesa(Despesa despesa)
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            despesa.id_usuario = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            if (despesa.id_usuario == 0) ret += "Usuario não informado ";
            if (despesa.id_categoria == 0) ret += "Categoria não informada ";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });


            var desp = await _despesaRepo.CreateDespesa(despesa);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);

        }

        //public Task UpdateDespesa(Despesa despesa);
        
        [HttpPost("UpdateDespesa")]
        [Authorize]
        public async Task<IActionResult> UpdateDespesa(Despesa despesa)
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            despesa.id_usuario = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            if (despesa.id_usuario == 0) ret += "Usuario não informado ";
            if (despesa.id_categoria == 0) ret += "Categoria não informada ";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.UpdateDespesa(despesa);
            if (desp == null) return BadRequest(new { desp = "ops!! não foi ralizado a alteração." });

            return Ok(desp);

        }

        
        [HttpPost]
        [Route("GetDespesasId")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetDespesasId([FromBody] Despesa despesa)
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            despesa.id_usuario = Convert.ToInt32(usuario[1].ToString());

            var ret = "";
            if (despesa.id_usuario == 0) ret += "usuario inválido";
            if (despesa.id == 0) ret += "Digite um Id válido";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetDespesasId(despesa);
            if (desp == null) return BadRequest(new { message = "Nehuma despesa localizada." });

            return Ok(desp);
        }


        [HttpPost("GetDespesas")]
        [Authorize]
        public async Task<IActionResult> GetDespesas([FromBody] Despesa despesa)
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            int Id_usuario = Convert.ToInt32(usuario[1].ToString());          

            var desp = await _despesaRepo.GetDespesas(Id_usuario, despesa.cd_mes, despesa.cd_ano);
            if (desp == null) return BadRequest(new { message = "Nehuma despesa localizada." });

            return Ok(desp);
        }


        [HttpPost("GetDespesasChart")]
        [Authorize]
        public async Task<IActionResult> GetDespesasChart([FromBody] Despesa despesa)
        {
            var user = User.Identity.Name;
            string[] usuario = user.Split(';');
            int Id_usuario = Convert.ToInt32(usuario[1].ToString());

            var desp = await _despesaRepo.GetDespesasChart(Id_usuario, despesa.cd_mes, despesa.cd_ano);
            if (desp == null) return BadRequest(new { message = "Nehuma despesa localizada." });

            return Ok(desp);
        }













        [HttpGet("GetUsuarioTipoDespesa")]
        [Authorize]
        public async Task<IActionResult> GetUsuarioNome(int id_usuario, int cd_tipoDespesa)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (cd_tipoDespesa == 0) ret += "Informe um tipo de despesa";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetUsuarioNome(id_usuario, cd_tipoDespesa);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);
        }


        //public Task DeleteDespesa(int id_usuario, int Id);
        [HttpPost("DeleteDespesa")]
        [Authorize]
        public async Task<IActionResult> DeleteDespesa(int id_usuario, int Id)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (Id == 0) ret += "Operação inválida";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.DeleteDespesa(id_usuario, Id);
            if (desp == null) return BadRequest(new { message = "Ops!! não foi localizado este registro." });

            return Ok(desp);

        }


        [HttpGet("GetDespesaMes")]
        [Authorize]
        public async Task<IActionResult> GetDespesasMes(int id_usuario, int cd_mes, int cd_ano)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (cd_mes == 0) ret += "Digite um mês valido";
            if (cd_ano == 0) ret += "Digite um ano valido";
            if (!String.IsNullOrWhiteSpace(ret)) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetDespesasMes(id_usuario, cd_mes, cd_ano);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);
        }

    }
}
