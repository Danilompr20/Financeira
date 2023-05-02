using Financeira.Domain.ViewModel;
using Financeira.RabbitMQSender;
using Financeira.Service;
using Financeira.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financeira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IRabbitSender _rabbitSender;
        public ClienteController( IClienteService clienteService, IRabbitSender rabbitSender)
        {
            _clienteService = clienteService;
            _rabbitSender = rabbitSender;
        }

        [HttpPost("novo-cliente")]
        public async Task<ActionResult> Adicionar([FromBody] ClienteViewModel cliente)
        {
            await _clienteService.AdicionarCliente(cliente);
            _rabbitSender.SendMessage(cliente, "clientequeue");
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
