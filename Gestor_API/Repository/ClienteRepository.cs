﻿using Dapper;
using Gestor_API.Context;
using Gestor_API.Contracts;
using Gestor_API.Entities;
using System;
using System.Threading.Tasks;

namespace Gestor_API.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DapperContext _context;

        public ClienteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            try
            {
                DateTime dat = Convert.ToDateTime(cliente.dt_nascimento);
                var dt_nascimento = dat.ToString("yyyy-MM-dd");

                DateTime dat2 = Convert.ToDateTime(cliente.dt_conjugue_nascimento);
                var dt_conjugue_nascimento = dat2.ToString("yyyy-MM-dd");

                if (dt_conjugue_nascimento == "0001-01-01")
                {
                    dt_conjugue_nascimento = null;
                }

                var query = @"INSERT INTO TB_CLIENTE (id_usuario,ds_nome,ds_cpf_cnpj,ds_rg_ie,fl_pj_pf,ds_email,ds_telefone,ds_celular,dt_nascimento,ds_cep,ds_endereco,ds_numero,ds_complemento,ds_bairro,ds_municipio,ds_estado,ds_conjugue_nome,ds_conjugue_cpf,ds_conjugue_rg,ds_conjugue_email,
                        ds_conjugue_telefone,ds_conjugue_celular,dt_conjugue_nascimento,fl_Ativo,ds_observacao,id_estado_civil) 
                        VALUES ('" + cliente.id_usuario + "','" + cliente.ds_nome + "','" + cliente.ds_cpf_cnpj + "','" + cliente.ds_rg_ie + "','" + cliente.fl_pj_pf + "','" + cliente.ds_email + "','" + cliente.ds_telefone + "','" + cliente.ds_celular
                            + "','" + dt_nascimento + "','" + cliente.ds_cep + "','" + cliente.ds_endereco + "','" + cliente.ds_numero + "','" + cliente.ds_complemento + "','" + cliente.ds_bairro + "','" + cliente.ds_municipio + "','" + cliente.ds_estado + "','" + cliente.ds_conjugue_nome + "','" + cliente.ds_conjugue_cpf + "','" + cliente.ds_conjugue_rg + "','" + cliente.ds_conjugue_email + "','" + cliente.ds_conjugue_telefone + "','" + cliente.ds_conjugue_celular
                            + "','" + dt_conjugue_nascimento + "','" + cliente.fl_Ativo + "','" + cliente.ds_observacao + "','" + cliente.id_estado_civil + "')" +
                            "SELECT CAST(SCOPE_IDENTITY() as int)";


                using var connection2 = _context.CreateConnection();
                var id = await connection2.QuerySingleAsync<int>(query);

                var createdCliente = new Cliente
                {
                    id = id,
                    id_usuario = cliente.id_usuario,
                    ds_nome = cliente.ds_nome,
                    ds_cpf_cnpj = cliente.ds_cpf_cnpj,
                    ds_rg_ie = cliente.ds_rg_ie,
                    fl_pj_pf = cliente.fl_pj_pf,
                    ds_email = cliente.ds_email,
                    ds_telefone = cliente.ds_telefone,
                    ds_celular = cliente.ds_celular,
                    dt_nascimento = cliente.dt_nascimento,
                    ds_cep = cliente.ds_cep,
                    ds_endereco = cliente.ds_endereco,
                    ds_numero = cliente.ds_numero,
                    ds_complemento = cliente.ds_complemento,
                    ds_bairro = cliente.ds_bairro,
                    ds_municipio = cliente.ds_municipio,
                    ds_estado = cliente.ds_estado,
                    ds_conjugue_nome = cliente.ds_conjugue_nome,
                    ds_conjugue_cpf = cliente.ds_conjugue_cpf,
                    ds_conjugue_rg = cliente.ds_conjugue_rg,
                    ds_conjugue_email = cliente.ds_conjugue_email,
                    ds_conjugue_telefone = cliente.ds_conjugue_telefone,
                    ds_conjugue_celular = cliente.ds_conjugue_celular,
                    dt_conjugue_nascimento = cliente.dt_conjugue_nascimento,
                    fl_Ativo = cliente.fl_Ativo,
                    ds_observacao = cliente.ds_observacao,
                    id_estado_civil = cliente.id_estado_civil,
                };
                return createdCliente;

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

    }
}