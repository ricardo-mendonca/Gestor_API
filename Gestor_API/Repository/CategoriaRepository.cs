using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities;
using Gestor_API.Entities.Categoria;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_API.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DapperContext _context;

        public CategoriaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategoria()
        {
            var query = "SELECT Id,ds_descricao FROM TB_CATEGORIA";
            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QueryAsync<Categoria>(query);
                return categoria.ToList();
            }
        }

        public async Task<Categoria> GetCategoriaId(int Id)
        {
            var query = "SELECT Id,ds_descricao FROM TB_CATEGORIA  where id = @Id ";
            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<Categoria>(query, new {Id});
                return categoria;
            }
        }

        public async Task<IEnumerable<Categoria>> GetCategoriaNome(string nome)
        {
            var query = "SELECT Id,ds_descricao FROM TB_CATEGORIA  where ds_descricao like '%" + nome + "%'"; 
            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QueryAsync<Categoria>(query);
                return categoria.ToList();
            }
        }
    }
}
