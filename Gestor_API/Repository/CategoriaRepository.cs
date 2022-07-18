using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities;
using Gestor_API.Entities.Categoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_API.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        #region CONTEXT
        private readonly DapperContext _context;
        public CategoriaRepository(DapperContext context)
        {
            _context = context;
        }
        #endregion


        public async Task<IEnumerable<Categoria>> GetCategoria()
        {
            var query = "SELECT Id,ds_descricao FROM TB_CATEGORIA";
            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QueryAsync<Categoria>(query);
                return categoria.ToList();
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
        public async Task<Categoria> GetCategoriaId(int Id)
        {
            var query = "SELECT Id,ds_descricao FROM TB_CATEGORIA  where id = @Id ";
            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<Categoria>(query, new { Id });
                return categoria;
            }
        }
        public async Task<Categoria> CreateCategoria(Categoria categoria)
        {
            var query = @"insert into tb_categoria(ds_descricao) values('"+categoria.ds_descricao+"')" +
                       "SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query);

            var CreateCategoria = new Categoria
            {
                Id = id,
                ds_descricao = categoria.ds_descricao,
            };
            return CreateCategoria;
        }
        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            try
            {

                var query = @"update tb_categoria set " +
                     " ds_descricao = '" + categoria.ds_descricao +
                    "' where id = " + categoria.Id;
                    
                using (var connection = _context.CreateConnection())
                {
                    await connection.QuerySingleOrDefaultAsync<Categoria>(query);
                    return categoria;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Categoria> DeletarCategoria(Categoria categoria)
        {
            try
            {
                var query = @"delete tb_categoria where id = " + categoria.Id;

                using (var connection = _context.CreateConnection())
                {
                    await connection.QuerySingleOrDefaultAsync<Categoria>(query);
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
