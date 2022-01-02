using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> Getusuarios()
        {
            var query = "SELECT Id,ds_nome,ds_email,ds_senha,ds_telefone,dt_nascimento,Status FROM TB_USUARIO";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QueryAsync<Usuario>(query);
                return usuario.ToList();
            }
        }

        public async Task<Usuario> GetUsuarioId(int id)
        {
            var query = "SELECT Id,ds_nome,ds_email,ds_senha,ds_telefone, Convert(varchar(10),dt_nascimento,103) as dt_nascimento,Status FROM TB_USUARIO where Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(query, new { id });
                return usuario;
            }
        }

        public async Task<IEnumerable<Usuario>> GetUsuarioNome(string nome)
        {
            string dsnome = nome;
            var query = "SELECT Id,ds_nome,ds_email,ds_senha,ds_telefone,Convert(varchar(10),dt_nascimento,103) as dt_nascimento,Status FROM TB_USUARIO where ds_nome like '%" + dsnome + "%'";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QueryAsync<Usuario>(query);
                return usuario.ToList();
            }
        }

        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            var query = "INSERT INTO TB_USUARIO (ds_nome,ds_email, ds_senha,ds_telefone,dt_nascimento,fl_status,cd_rg,cd_cpf,cd_cep,ds_endereco,ds_complemento,nr_endereco,ds_bairro,ds_cidade,cd_uf,dt_inclusao) " +
                "VALUES (@ds_nome,@ds_email, @ds_senha,@ds_telefone,@dt_nascimento,@fl_status,@cd_rg,@cd_cpf,@cd_cep,@ds_endereco,@ds_complemento,@nr_endereco,@ds_bairro,@ds_cidade,@cd_uf,getdate())" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("ds_nome", usuario.ds_nome, DbType.String);
            parameters.Add("ds_email", usuario.ds_email, DbType.String);
            parameters.Add("ds_senha", usuario.ds_senha, DbType.String);
            parameters.Add("ds_telefone", usuario.ds_telefone, DbType.String);
            parameters.Add("dt_nascimento", usuario.dt_nascimento);
            parameters.Add("fl_status", usuario.fl_status);
            parameters.Add("cd_rg", usuario.cd_rg);
            parameters.Add("cd_cpf", usuario.cd_cpf);
            parameters.Add("cd_cep", usuario.cd_cep);
            parameters.Add("ds_endereco", usuario.ds_endereco);
            parameters.Add("ds_complemento", usuario.ds_complemento);
            parameters.Add("nr_endereco", usuario.nr_endereco);
            parameters.Add("ds_bairro", usuario.ds_bairro);
            parameters.Add("ds_cidade", usuario.ds_cidade);
            parameters.Add("cd_uf", usuario.cd_uf);
            parameters.Add("dt_inclusao", usuario.dt_inclusao);




            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdUsuario = new Usuario
                {
                    Id = id,
                    ds_nome = usuario.ds_nome,
                    ds_email = usuario.ds_email,
                    ds_senha = usuario.ds_senha,
                    ds_telefone = usuario.ds_telefone,
                    dt_nascimento = usuario.dt_nascimento,
                    fl_status = usuario.fl_status,
                    cd_rg = usuario.cd_rg,
                    cd_cpf = usuario.cd_cpf,
                    cd_cep = usuario.cd_cep,
                    ds_endereco = usuario.ds_endereco,
                    ds_complemento = usuario.ds_complemento,
                    nr_endereco = usuario.nr_endereco,
                    ds_bairro = usuario.ds_bairro,
                    ds_cidade = usuario.ds_cidade,
                    cd_uf = usuario.cd_uf,
                    dt_inclusao = usuario.dt_inclusao

                };

                return createdUsuario;
            }
        }
    }
}
