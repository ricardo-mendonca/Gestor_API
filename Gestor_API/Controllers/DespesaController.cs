using Gestor_API.Contracts;
using Gestor_API.Entities.Contas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    //[Route("v1/")]
    [ApiController]
    public class DespesaController : Controller
    {
        private readonly IDespesaRepository _despesaRepo;

        public DespesaController(IDespesaRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;

        }

        [HttpPost("v1/CreateDespesa")]
        public async Task<IActionResult> CreateDespesa(Despesa despesa)
        {

            var desp = await _despesaRepo.CreateDespesa(despesa);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);

        }

        [HttpGet("v1/GetDespesaMes")]
        public async Task<IActionResult> GetDespesas(int id_usuario, int cd_mes, int cd_ano)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (cd_mes == 0) ret += "Digite um mês valido";
            if (cd_ano == 0) ret += "Digite um ano valido";
            if (ret != null) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetDespesas(id_usuario, cd_mes, cd_ano);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);
        }

        [HttpGet("v1/GetDespesasId")]
        public async Task<IActionResult> GetDespesasId(int id_usuario, int Id)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (Id == 0) ret += "Digite um Id válido";
            if (ret != null) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetDespesasId(id_usuario, Id);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);
        }

        [HttpGet("v1/GetUsuarioNome")]
        public async Task<IActionResult> GetUsuarioNome(int id_usuario, int cd_tipo_despesa)
        {
            var ret = "";
            if (id_usuario == 0) ret += "usuario inválido";
            if (cd_tipo_despesa == 0) ret += "Informe um tipo de despesa";
            if (ret != null) return BadRequest(new { message = ret });

            var desp = await _despesaRepo.GetUsuarioNome(id_usuario, cd_tipo_despesa);
            if (desp == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(desp);
        }

        
        //public Task UpdateDespesa(Despesa despesa);
        
        //public Task DeleteDespesa(int id_usuario, int Id);

    }
}
