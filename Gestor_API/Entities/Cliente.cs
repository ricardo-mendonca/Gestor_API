using System;
using System.ComponentModel.DataAnnotations;

namespace Gestor_API.Entities
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string ds_nome { get; set; }
        public string ds_cpf_cnpj { get; set; }
        public string ds_rg_ie { get; set; }
        public char fl_pj_pf { get; set; }
        public string ds_email { get; set; }
        public string ds_telefone { get; set; }
        public string ds_celular { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string ds_cep { get; set; }
        public string ds_endereco { get; set; }
        public string ds_numero { get; set; }
        public string ds_complemento { get; set; }
        public string ds_bairro { get; set; }
        public string ds_municipio { get; set; }
        public string ds_estado { get; set; }
        public string ds_conjugue_nome { get; set; }
        public string ds_conjugue_cpf { get; set; }
        public string ds_conjugue_rg { get; set; }
        public string ds_conjugue_email { get; set; }
        public string ds_conjugue_telefone { get; set; }
        public string ds_conjugue_celular { get; set; }
        public DateTime dt_conjugue_nascimento { get; set; }
        public char fl_Ativo { get; set; }
        public string ds_observacao { get; set; }
        public int id_estado_civil { get; set; }

    }
}
