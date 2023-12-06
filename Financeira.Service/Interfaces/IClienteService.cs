
using Financeira.Domain.ViewModel;


namespace Financeira.Service.Interfaces
{
    public interface IClienteService
    {
        Task AdicionarCliente(ClienteViewModel cliente);
        Task<List<ClienteViewModel>> ListarClientesByEstado();
        Task<List<ClienteViewModel>> ListarClientesQuePossuemParcelasPagasSemAtraso();
    }
}
