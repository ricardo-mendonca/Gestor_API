using Gestor_API.Entities.Categoria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor_API.Contracts
{
    public interface ICategoriaRepository
    {
        public Task<IEnumerable<Categoria>> GetCategoria();

		public Task<Categoria> GetCategoriaId(int Id);

		public Task<IEnumerable<Categoria>> GetCategoriaNome(string nome);

		//public Task<Usuario> CreateUsuario(Usuario usuario);

		//public Task UpdateCompany(int id, UsuarioForUpdateDto company);
		//public Task DeleteCompany(int id);
		//public Task<Usuario> GetCompanyByEmployeeId(int id);
		//public Task<Usuario> GetCompanyEmployeesMultipleResults(int id);
	}
}
