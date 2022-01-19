using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestor_API.Controllers
{
    public class Valida
    {
        public Valida()
        {
            Validacoes = new List<Valida>();
        }

        [NotMapped]
        public string? NomePropriedade { get; set; }

        [NotMapped]
        public string? Informacao { get; set; }

        public List<Valida> Validacoes;


        public bool ValidapropriedadeString(string valor, string nomePropriedade)
        {

            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Validacoes.Add(new Valida
                {
                    Informacao = "Dgite um valor",
                    NomePropriedade = nomePropriedade,
                });
                return false;
            }
            return true;

        }

        public bool ValidapropriedadeInt(int valor, string nomePropriedade)
        {

            if (valor == 0 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Validacoes.Add(new Valida
                {
                    Informacao = "Informe um valor",
                    NomePropriedade = nomePropriedade,
                });
                return false;
            }
            return true;

        }

    }
}
