using Financeira.Domain.ViewModel;
using Financeira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financeira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
       
        public ClienteController( IClienteService clienteService)
        {
            _clienteService = clienteService;
           
        }

        [HttpPost("novo-cliente")]
        public async Task<ActionResult> Adicionar([FromBody] ClienteViewModel cliente)
        {
            await _clienteService.AdicionarCliente(cliente);
            return Ok("Sucesso");

        }


        [HttpGet("buscar-estado")]
        public async Task<ActionResult> BuscarPorEstado()
        {
            return Ok(_clienteService.ListarClientesByEstado());
        }


        [HttpGet("buscar-parcelas")]
        public async Task<ActionResult> BuscarPorParcelasPagas()
        {
            return Ok(_clienteService.ListarClientesQuePossuemParcelasPagasSemAtraso());
        }
    }
}
