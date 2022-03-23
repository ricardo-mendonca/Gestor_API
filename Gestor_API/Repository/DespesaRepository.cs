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


        public async Task<Despesa> GetDespesasId(Despesa despesa)
        {
            var query = @"SELECT * FROM TB_DESPESA where id_usuario =  " + despesa.id_usuario + " and id = " + despesa.id;

            using var connection = _context.CreateConnection();
            despesa = await connection.QuerySingleOrDefaultAsync<Despesa>(query);
            return despesa;

        }

        public async Task<Despesa> CreateDespesa(Despesa despesa)
        {
            Guid guid = Guid.NewGuid();

            DateTime dat = Convert.ToDateTime(despesa.dt_vencimento);
            var dt_vencimento = dat.ToString("yyyy-MM-dd");
            var dt_pagamento = "2100-01-01";
            if (despesa.fl_pago == 1)
            {
                DateTime dt = DateTime.Now;
                dt_pagamento = dt.ToString("yyyy-MM-dd");
            }

            string vl_valor_parc = despesa.vl_valor_parc.ToString(CultureInfo.InvariantCulture);
            string vl_valor_desconto = despesa.vl_valor_desconto.ToString(CultureInfo.InvariantCulture);
            string vl_valor_multa = despesa.vl_valor_multa.ToString(CultureInfo.InvariantCulture);



            if (despesa.fl_despesa_fixa == '1')
            {
                int qtd = 18; //quantidade delancamentos futuros de conta fixa

                for (int i = 0; i < qtd; i++)
                {
                    int y = i + 1;

                    if (y > 1)
                    {
                        dt_vencimento = despesa.dt_vencimento.AddMonths(i).ToString("yyyy-MM-dd");
                    }

                    var query2 = @"insert into TB_DESPESA(  id_usuario ,   id_categoria ,   cd_qtd_parc ,   cd_qtd_tot_parc ,   vl_valor_parc ,   vl_valor_multa ,   vl_valor_desconto ,   cd_dia ,   cd_mes ,   cd_ano ,   fl_despesa_fixa ,   fl_pago ,   dt_vencimento ,   dt_pagamento ,dt_cadastro ,ds_descricao,id_parcela)
                                        values(" + despesa.id_usuario + ", " + despesa.id_categoria + ", " + 1 + ", " + 1 +
                                        ", " + vl_valor_parc + ", " + vl_valor_multa + ", " + vl_valor_desconto +
                                        ", " + dt_vencimento.Substring(8, 2) + ", " + dt_vencimento.Substring(5, 2) + ", " + dt_vencimento.Substring(0, 4) +
                                        ", " + despesa.fl_despesa_fixa + ", " + despesa.fl_pago + ", '" + dt_vencimento + "', '" + dt_pagamento + "', getdate(), '" + despesa.ds_descricao + "','" + guid + "' )" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
                    using var connection2 = _context.CreateConnection();
                    despesa.id = await connection2.QuerySingleAsync<int>(query2);
                }



            }
            else
            {
                int qtd = Convert.ToInt32(despesa.cd_qtd_tot_parc);

                for (int i = 0; i < qtd; i++)
                {
                    int y = i + 1;

                    if (y > 1)
                    {
                        dt_vencimento = despesa.dt_vencimento.AddMonths(i).ToString("yyyy-MM-dd");
                    }

                    var query2 = @"insert into TB_DESPESA(  id_usuario ,   id_categoria ,   cd_qtd_parc ,   cd_qtd_tot_parc ,   vl_valor_parc ,   vl_valor_multa ,   vl_valor_desconto ,   cd_dia ,   cd_mes ,   cd_ano ,   fl_despesa_fixa ,   fl_pago ,   dt_vencimento ,   dt_pagamento ,dt_cadastro ,ds_descricao, id_parcela)
                                        values(" + despesa.id_usuario + ", " + despesa.id_categoria + ", " + y + ", " + despesa.cd_qtd_tot_parc +
                                         ", " + vl_valor_parc + ", " + vl_valor_multa + ", " + vl_valor_desconto +
                                         ", " + dt_vencimento.Substring(8, 2) + ", " + dt_vencimento.Substring(5, 2) + ", " + dt_vencimento.Substring(0, 4) +
                                         ", " + despesa.fl_despesa_fixa + ", " + despesa.fl_pago + ", '" + dt_vencimento + "', '" + dt_pagamento + "', getdate(), '" + despesa.ds_descricao + "', '" + guid + "' )" +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";

                    using (var connection2 = _context.CreateConnection())
                    {
                        var id = await connection2.QuerySingleAsync<int>(query2);
                    }
                }
            }

            using (var connection = _context.CreateConnection())
            {
                //var id = await connection.QuerySingleAsync<int>(query);

                var createdDespesa = new Despesa
                {
                    id = despesa.id,
                    id_usuario = despesa.id_usuario,
                    id_categoria = despesa.id_categoria,
                    cd_qtd_parc = despesa.cd_qtd_parc,
                    cd_qtd_tot_parc = despesa.cd_qtd_tot_parc,
                    vl_valor_parc = despesa.vl_valor_parc,
                    vl_valor_multa = despesa.vl_valor_multa,
                    vl_valor_desconto = despesa.vl_valor_desconto,
                    cd_dia = despesa.cd_dia,
                    cd_mes = despesa.cd_mes,
                    cd_ano = despesa.cd_ano,
                    fl_despesa_fixa = despesa.fl_despesa_fixa,
                    fl_pago = despesa.fl_pago,
                    dt_vencimento = despesa.dt_vencimento,
                    dt_pagamento = despesa.dt_pagamento,
                    dt_cadastro = despesa.dt_cadastro,
                    dt_alteracao = despesa.dt_alteracao,
                    ds_descricao = despesa.ds_descricao

                };

                return createdDespesa;
            }
        }

        public async Task<Despesa> UpdateDespesa(Despesa despesa)
        {
            try
            {
                DateTime dat = Convert.ToDateTime(despesa.dt_vencimento);
                var dt_vencimento = dat.ToString("yyyy-MM-dd");
                var dt_pagamento = "2100-01-01";
                if (despesa.fl_pago == 1)
                {
                    DateTime dt = DateTime.Now;
                    dt_pagamento = dt.ToString("yyyy-MM-dd");
                }

                string vl_valor_parc = despesa.vl_valor_parc.ToString(CultureInfo.InvariantCulture);
                string vl_valor_desconto = despesa.vl_valor_desconto.ToString(CultureInfo.InvariantCulture);
                string vl_valor_multa = despesa.vl_valor_multa.ToString(CultureInfo.InvariantCulture);



                var query = @"update TB_DESPESA set 
                            id_categoria = " + despesa.id_categoria +
                                " ,vl_valor_parc = " + despesa.vl_valor_parc +
                                " ,vl_valor_multa=" + despesa +
                                " ,vl_valor_desconto=" + despesa.vl_valor_desconto +
                                " ,cd_dia=" + despesa.cd_dia +
                                " ,cd_mes=" + despesa.cd_mes +
                                " ,cd_ano=" + despesa.cd_ano +
                                " ,fl_pago=" + despesa.fl_pago +
                                " ,dt_vencimento=" + despesa.dt_vencimento +
                                " ,dt_pagamento=" + despesa.dt_pagamento +
                                " ,ds_descricao=" + despesa.ds_descricao +

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






        public async Task<IEnumerable<Despesa>> GetDespesasMes(int id_usuario, int cd_mes, int cd_ano)
        {

            var query = @"SELECT * FROM TB_DESPESA where id_usuario = " + id_usuario + " and cd_mes = " + cd_mes + " and cd_ano = " + cd_ano;
            using (var connection = _context.CreateConnection())
            {
                var despesa = await connection.QueryAsync<Despesa>(query);
                return despesa.ToList();
            }
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



        public async Task<IEnumerable<Despesa>> GetDespesas(int id_usuario, int cd_mes, int cd_ano)
        {
            var query = @"select Id, id_categoria, cd_qtd_parc,cd_qtd_tot_parc, vl_valor_parc, vl_valor_desconto, vl_valor_multa, cd_dia, cd_mes, cd_ano, fl_despesa_fixa,fl_pago,dt_vencimento,ds_descricao 
                        from TB_DESPESA where id_usuario = " + id_usuario + " and cd_mes = " + cd_mes + " and cd_ano = " + cd_ano;
            using (var connection = _context.CreateConnection())
            {
                var despesa = await connection.QueryAsync<Despesa>(query);
                return despesa.ToList();
            }
        }





    }
}
