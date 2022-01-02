using Gestor_API.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Gestor_API.Entities.Contas
{
    public class Despesa 
    {
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Mês")]
        public int Mes { get; set; }

        [Display(Name = "TipoDespesa")]
        public EnumTipoDespesa TipoDespesa { get; set; }


    }
}
