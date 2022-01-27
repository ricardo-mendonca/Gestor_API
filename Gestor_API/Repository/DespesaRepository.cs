using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities.Contas;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            DateTime dat = Convert.ToDateTime(despesa.dt_vencimento);
            var dt = dat.ToString("yyyy-MM-dd");
                        
            string vl = despesa.vl_valor.ToString(CultureInfo.InvariantCulture);

            var query = @"insert into TB_DESPESA (id_usuario, id_categoria, vl_valor, cd_tipoDespesa, cd_mes, cd_ano, dt_cadastro, dt_vencimento, fl_pago, ds_descricao) 
                        values (" + despesa.id_usuario + ", " + despesa.id_categoria + ", " + vl + ", " + (int)despesa.cd_tipoDespesa + ", "
                        + despesa.cd_mes + ", " + despesa.cd_ano + ", getdate() , " + dt + ", " + despesa.fl_pago + ", '"+despesa.ds_descricao+"') " +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query);

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
                    dt_vencimento = despesa.dt_vencimento,
                    fl_pago = despesa.fl_pago,
                    ds_descricao = despesa.ds_descricao,
                };

                return createdDespesa;
            }
        }



        public async Task<IEnumerable<Despesa>> GetDespesas(int id_usuario, int cd_mes, int cd_ano)
        {

            var query = @"SELECT * FROM TB_DESPESA where id_usuario = " + id_usuario + " and cd_mes = " + cd_mes + " and cd_ano = " + cd_ano;
            using (var connection = _context.CreateConnection())
            {
                var despesa = await connection.QueryAsync<Despesa>(query);
                return despesa.ToList();
            }
        }

        public async Task<Despesa> GetDespesasId(int id_usuario, int Id)
        {
            var query = @"SELECT * FROM TB_DESPESA where id_usuario =  " + id_usuario + " and id = " + Id;

            using var connection = _context.CreateConnection();
            var despesa = await connection.QuerySingleOrDefaultAsync<Despesa>(query);
            return despesa;
        }

        public async Task<IEnumerable<Despesa>> GetUsuarioNome(int id_usuario, int cd_tipoDespesa)
        {
            var query = @"SELECT * FROM TB_DESPESA where id_usuario =  " + id_usuario + " and cd_tipoDespesa = " + cd_tipoDespesa;
            using (var connection = _context.CreateConnection())
            {
                var despesa = await connection.QueryAsync<Despesa>(query);
                return despesa.ToList();
            }
        }

        public async Task<Despesa> DeleteDespesa(int id_usuario, int Id)
        {
            try
            {
                var query2 = @"SELECT *  FROM TB_DESPESA where id_usuario =  " + id_usuario + " and id = " + Id;

                using var connection2 = _context.CreateConnection();
                var ret = await connection2.QuerySingleOrDefaultAsync<Despesa>(query2);

                if (ret != null)
                {
                    var query = @"Delete TB_DESPESA where id_usuario =  " + id_usuario + " and id = " + Id;
                    using (var connection = _context.CreateConnection())
                    {
                         await connection.QuerySingleOrDefaultAsync<Despesa>(query);
                        return ret;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<Despesa> UpdateDespesa(Despesa despesa)
        {
            try
            {
                DateTime dat = Convert.ToDateTime(despesa.dt_vencimento);
                var dt = dat.ToString("yyyy-MM-dd");

                string vl = despesa.vl_valor.ToString(CultureInfo.InvariantCulture);

                var query = @"update TB_DESPESA set 
                            id_categoria = " + despesa.id_categoria +
                                " ,vl_valor = " + vl +
                                " ,cd_mes = " + despesa.cd_mes +
                                " ,cd_ano = " + despesa.cd_ano +
                                " ,dt_alteracao = getdate() " +
                                " ,dt_vencimento = '" + dt +
                                "',fl_pago = " + despesa.fl_pago +
                                " ,cd_tipoDespesa = " + (int)despesa.cd_tipoDespesa +
                                " ,ds_descricao = '" + despesa.ds_descricao +
                                "' where id= " + despesa.id +
                                " and id_usuario = " + despesa.id_usuario;
                using (var connection = _context.CreateConnection())
                {
                     await connection.QuerySingleOrDefaultAsync<Despesa>(query);
                    return despesa;
                }
            }
            catch (Exception)
            {
                return null;
            }
         
        }
    }
}
