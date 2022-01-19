using Gestor_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor_API.Contracts
{
    public interface IUsuarioRepository 
    {
		public Task<IEnumerable<Usuario>> Getusuarios();
		public Task<Usuario> GetUsuarioId(int Id);

		public Task<IEnumerable<Usuario>> GetUsuarioNome(string nome);

		public Task<Usuario> CreateUsuario(Usuario usuario);

		//public Task UpdateCompany(int id, UsuarioForUpdateDto company);
		//public Task DeleteCompany(int id);
		//public Task<Usuario> GetCompanyByEmployeeId(int id);
		//public Task<Usuario> GetCompanyEmployeesMultipleResults(int id);



	}
}
