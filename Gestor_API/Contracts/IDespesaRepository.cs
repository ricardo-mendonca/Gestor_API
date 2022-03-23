using Gestor_API.Entities.Contas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor_API.Contracts
{
    public interface IDespesaRepository
    {

		public Task<Despesa> CreateDespesa(Despesa despesa);

		public Task<Despesa> UpdateDespesa(Despesa despesa);
		public Task<Despesa> DeleteDespesa(int id_usuario, int Id);
		public Task<IEnumerable<Despesa>> GetDespesasMes(int id_usuario, int cd_mes, int cd_ano );
		public Task<Despesa> GetDespesasId(Despesa despesa);
		public Task<IEnumerable<Despesa>> GetUsuarioNome(int id_usuario,int cd_tipo_despesa);

		public Task<IEnumerable<Despesa>> GetDespesas(int id_usuario, int cd_mes, int cd_ano);

	}
}
