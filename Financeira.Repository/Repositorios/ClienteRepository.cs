using Financeira.Domain.Entity;
using Financeira.Repository.Context;
using Financeira.Repository.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {

        protected readonly ContextoDB _context;

        public ClienteRepository(ContextoDB contexto)
        {
            _context = contexto;
        }
        public async Task AdicionarCliente(Cliente cliente)
        {
             _context.Add(cliente);
             _context.SaveChanges();
        }

        public Task<List<Cliente>> ListarClientesByEstado()
        {
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> ListarClientesQuePossuemParcelasPagasSemAtraso()
        {
            throw new NotImplementedException();
        }
    }
}
