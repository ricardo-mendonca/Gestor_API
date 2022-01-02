using System;
using System.ComponentModel.DataAnnotations;

namespace Gestor_API.Entities
{
    public class Usuario 
    {
        [Key]
        public int Id { get; set; }
        public string ds_nome { get; set; }
        public string ds_email { get; set; }
        public string ds_senha { get; set; }
        public string ds_telefone { get; set; }
        public string dt_nascimento { get; set; }
        public char fl_status { get; set; }
        public string cd_rg { get; set; }
        public double   cd_cpf { get; set; }
        public string cd_cep { get; set; }
        public string ds_endereco { get; set; }
        public string ds_complemento { get; set; }
        public string nr_endereco { get; set; }
        public string ds_bairro { get; set; }
        public string ds_cidade { get; set; }
        public string cd_uf { get; set; }
        public DateTime dt_inclusao { get; set; }
	}
}
