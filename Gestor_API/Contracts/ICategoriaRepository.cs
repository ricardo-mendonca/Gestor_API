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

		public Task<Categoria> CreateCategoria(Categoria categoria);

		public Task<Categoria> UpdateCategoria(Categoria categoria);

		public Task<Categoria> DeletarCategoria(Categoria categoria);

	}
}
