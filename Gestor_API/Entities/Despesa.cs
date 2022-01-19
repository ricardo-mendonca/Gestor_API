using Gestor_API.Entities.Enum;
using System;

namespace Gestor_API.Entities.Contas
{
    public class Despesa
    {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public int id_categoria { get; set; }
        public decimal vl_valor { get; set; }
        public int cd_mes { get; set; }
        public int cd_ano { get; set; }
        public DateTime dt_cadastro { get; set; }
        public DateTime dt_alteracao { get; set; }
        public DateTime dt_pagamento { get; set; }
        public DateTime dt_vencimento { get; set; }
        public char fl_pago { get; set; }
        public EnumTipoDespesa cd_tipoDespesa { get; set; }

    }
}
