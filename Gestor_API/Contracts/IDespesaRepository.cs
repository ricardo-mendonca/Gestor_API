using Gestor_API.Entities.Contas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor_API.Contracts
{
    public interface IDespesaRepository
    {

		public Task<Despesa> CreateDespesa(Despesa despesa);
		public Task<IEnumerable<Despesa>> GetDespesas(int id_usuario, int cd_mes, int cd_ano );



		public Task<Despesa> GetDespesasId(int id_usuario,int Id);
		public Task<IEnumerable<Despesa>> GetUsuarioNome(int id_usuario,int cd_tipo_despesa);
		public Task UpdateDespesa(Despesa despesa);
		public Task DeleteDespesa(int id_usuario, int Id);
		//public Task<Usuario> GetCompanyByEmployeeId(int id);
		//public Task<Usuario> GetCompanyEmployeesMultipleResults(int id);
	}
}
