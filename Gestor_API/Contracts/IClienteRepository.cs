﻿using Gestor_API.Entities;
using System.Threading.Tasks;

namespace Gestor_API.Contracts
{
    public interface IClienteRepository
    {
        public Task<Cliente> CreateCliente(Cliente cliente);
    }
}
