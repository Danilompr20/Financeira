using Financeira.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Repositorios.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarCliente(Cliente cliente);
        Task<List<Cliente>> ListarClientesByEstado();
        Task<List<Cliente>> ListarClientesQuePossuemParcelasPagasSemAtraso();
    }
}
