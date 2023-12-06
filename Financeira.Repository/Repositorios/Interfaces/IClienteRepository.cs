using Financeira.Domain.Entity;


namespace Financeira.Repository.Repositorios.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarCliente(Cliente cliente);
        Task<List<Cliente>> ListarClientesByEstado();
        Task<List<Cliente>> ListarClientesQuePossuemParcelasPagasSemAtraso();
    }
}
