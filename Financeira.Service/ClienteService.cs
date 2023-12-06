using Financeira.Domain.Entity;
using Financeira.Domain.ViewModel;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service.Interfaces;

namespace Financeira.Service
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task AdicionarCliente(ClienteViewModel clienteView)
        {
            var cliente = new Cliente();
            cliente.UF = clienteView.UF;
            cliente.CPF = clienteView.CPF;
            cliente.Celular = clienteView.Celular;
            cliente.Nome = clienteView.Nome;


            try
            {
                await _clienteRepository.AdicionarCliente(cliente);
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }
         
        }

        public async Task<List<ClienteViewModel>> ListarClientesByEstado()
        {
            try
            {
                var cliente = await _clienteRepository.ListarClientesByEstado();
                var clienteView = new List<ClienteViewModel>();
                if (cliente is not null)
                {
                    cliente.ForEach(x => clienteView.Add(
                        new ClienteViewModel()
                        {
                            Celular = x.Celular,
                            CPF = x.CPF,
                            Nome = x.Nome,
                            UF = x.UF
                        }));
                }

                return clienteView;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<ClienteViewModel>> ListarClientesQuePossuemParcelasPagasSemAtraso()
        {
            try
            {
                var cliente = await _clienteRepository.ListarClientesQuePossuemParcelasPagasSemAtraso();
                var clienteView = new List<ClienteViewModel>();
                if (cliente is not null)
                {
                    cliente.ForEach(x => clienteView.Add(
                        new ClienteViewModel()
                        {
                            Celular = x.Celular,
                            CPF = x.CPF,
                            Nome = x.Nome,
                            UF = x.UF
                        }));
                }

                return clienteView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
