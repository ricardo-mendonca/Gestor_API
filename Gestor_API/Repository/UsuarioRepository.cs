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


        public async Task<Usuario> login(string ds_email, string ds_senha)
        {
            var query = "SELECT * FROM TB_USUARIO where ds_email = '" + ds_email + "' and ds_senha = '" + ds_senha + "'";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(query);
                return usuario;
            }
        }

        public async Task<IEnumerable<Usuario>> Getusuarios()
        {
            var query = "SELECT Id,ds_nome,ds_email,ds_senha,ds_telefone,dt_nascimento,fl_status FROM TB_USUARIO";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QueryAsync<Usuario>(query);
                return usuario.ToList();
            }
        }

        public async Task<Usuario> GetUsuarioId(int id)
        {

            var query = "select ds_nome,ds_email,ds_senha,ds_telefone,  dt_nascimento,fl_status,cd_rg,cd_cpf,cd_cep,ds_endereco,ds_complemento,nr_endereco,ds_bairro,ds_cidade,cd_uf" +
                " FROM TB_USUARIO where Id = "+ id ;

            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(query);
                return usuario;
            }
        }

        public async Task<IEnumerable<Usuario>> GetUsuarioNome(string nome)
        {
            string dsnome = nome;
            var query = "SELECT Id,ds_nome,ds_email,ds_senha,ds_telefone,Convert(varchar(10),dt_nascimento,103) as dt_nascimento,fl_status FROM TB_USUARIO where ds_nome like '%" + dsnome + "%'";
            using (var connection = _context.CreateConnection())
            {
                var usuario = await connection.QueryAsync<Usuario>(query);
                return usuario.ToList();
            }
        }

        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {
            var query = @"INSERT INTO TB_USUARIO (ds_nome,ds_email, ds_senha,ds_telefone,dt_nascimento,fl_status,cd_rg,cd_cpf,cd_cep,ds_endereco,ds_complemento,nr_endereco,ds_bairro,ds_cidade,cd_uf,dt_inclusao) 
                VALUES ('" + usuario.ds_nome + "','" + usuario.ds_email + "','" + usuario.ds_senha + "','" + usuario.ds_telefone + "','" + usuario.dt_nascimento + "','1','" + usuario.cd_rg + "','" + usuario.cd_cpf + "','" + usuario.cd_cep +
                "','" + usuario.ds_endereco + "','" + usuario.ds_complemento + "','" + usuario.nr_endereco + "','" + usuario.ds_bairro + "','" + usuario.ds_cidade + "','" + usuario.cd_uf + "',getdate()) " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection2 = _context.CreateConnection();
            var id = await connection2.QuerySingleAsync<int>(query);


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

        public async Task<Usuario> updateUsuario(Usuario usuario)
        {
            var query = @"update TB_USUARIO set ds_nome='"+usuario.ds_nome+ "' ,ds_email= '" + usuario.ds_email + "', ds_telefone= '" + usuario.ds_telefone + "' ,dt_nascimento= '" + usuario.dt_nascimento +
                "' ,cd_rg='" + usuario.cd_rg + "' , cd_cep='" + usuario.cd_cep + "' ,ds_endereco='" + usuario.ds_endereco + "' ,ds_complemento='" + usuario.ds_complemento +
                "' ,nr_endereco='" + usuario.nr_endereco + "',ds_bairro='" + usuario.ds_bairro + "',ds_cidade='" + usuario.ds_cidade + "',cd_uf='" + usuario.cd_uf + "' where id = "+ usuario.Id + " SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<Usuario>(query);
            }

            var createdUsuario = new Usuario
            {
                Id = usuario.Id,
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

        public async Task<Usuario> ResetPassword(Usuario usuario)
        {
            var query = @"select COUNT(1) from TB_USUARIO where ds_email = '" + usuario.ds_email + "' "; 
            
            using var connection2 = _context.CreateConnection();
            int count = await connection2.QuerySingleAsync<int>(query);
            
            if(count > 0)
            {
                query = null;
                query = "update tb_usuario set ds_senha = '654321' where  ds_email = '" + usuario.ds_email + "' " + "SELECT CAST(SCOPE_IDENTITY() as int)";
                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QuerySingleAsync<Usuario>(query);
                    return user;
                }
            }

            var createdUsuario = new Usuario
            {
                ds_nome = "erro nenhum email encontrado."
            };
            return createdUsuario;
        }

        
    }


}

