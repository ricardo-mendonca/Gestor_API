using Gestor_API.Contracts;
using Gestor_API.Entities.Contas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestor_API.Controllers
{
    [Route("v1/Despesa")]
    [ApiController]
    public class DespesaController : Controller
    {
        private readonly IDespesaRepository _despesaRepo;

        public DespesaController(IDespesaRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDespesa(Despesa despesa)
        {

            var usuarios = await _despesaRepo.CreateDespesa(despesa);
            if (usuarios == null) return BadRequest(new { message = "Nehum usuário localizado." });

            return Ok(usuarios);

        }


    }
}
