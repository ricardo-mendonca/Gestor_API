using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities.Contas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor_API.Repository
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly DapperContext _context;

        public DespesaRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<Despesa> CreateDespesa(Despesa despesa)
        {
            var query = @"insert into TB_DESPESA (id_usuario,id_categoria,vl_valor,cd_tipoDespesa,cd_mes,cd_ano,dt_cadastro,dt_alteracao,dt_vencimento,fl_pago) 
                        values (@id_usuario,@id_categoria,@vl_valor,@cd_tipoDespesa,@cd_mes,@cd_ano,getdate(),getdate(),@dt_vencimento,0) ";

            var parameters = new DynamicParameters();
            //parameters.Add("id", despesa.id);
            parameters.Add("id_usuario", despesa.id_usuario);
            parameters.Add("id_categoria", despesa.id_categoria);
            parameters.Add("vl_valor", despesa.vl_valor);
            parameters.Add("cd_tipoDespesa", despesa.cd_tipoDespesa);
            parameters.Add("cd_mes", despesa.cd_mes);
            parameters.Add("cd_ano", despesa.cd_ano);
            parameters.Add("dt_cadastro", despesa.dt_cadastro);
            parameters.Add("dt_alteracao", despesa.dt_alteracao);
            parameters.Add("dt_vencimento", despesa.dt_vencimento);
            parameters.Add("fl_pago", despesa.fl_pago);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdDespesa = new Despesa
                {
                    id = id,
                    id_usuario = despesa.id_usuario,
                    id_categoria = despesa.id_categoria,
                    vl_valor = despesa.vl_valor,
                    cd_tipoDespesa = despesa.cd_tipoDespesa,
                    cd_mes = despesa.cd_mes,
                    cd_ano = despesa.cd_ano,
                    dt_cadastro = despesa.dt_cadastro,
                    dt_alteracao = despesa.dt_alteracao,
                    dt_vencimento = despesa.dt_vencimento,
                    fl_pago = despesa.fl_pago,
                };

                return createdDespesa;
            }
        }

        public async Task DeleteDespesa(int id_usuario, int Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Despesa>> GetDespesas(int id_usuario, int cd_mes, int cd_ano)
        {

            var query = @"SELECT * FROM TB_DESPESA where id_usuario = @id_usuario and cd_mes = @cd_mes and cd_ano = @cd_ano";
            using (var connection = _context.CreateConnection())
            {
                var despesa = await connection.QueryAsync<Despesa>(query);
                return despesa.ToList();
            }
        }

        public async Task<Despesa> GetDespesasId(int id_usuario, int Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Despesa>> GetUsuarioNome(int id_usuario, int cd_tipo_despesa)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateDespesa(Despesa despesa)
        {
            throw new System.NotImplementedException();
        }
    }
}
