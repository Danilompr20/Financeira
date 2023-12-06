using Financeira.Domain.ViewModel;
using Financeira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financeira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciamentoController : ControllerBase
    {
        private readonly IFinanciamentoService _financiamentoService;
        private readonly IParcelaService _parcelaService;

        public FinanciamentoController(IFinanciamentoService financiamentoService, IParcelaService parcelaService)
        {
            _financiamentoService = financiamentoService;
            _parcelaService = parcelaService;
        }

        [HttpPost("novo")]
        public async Task<ActionResult> Adicionar([FromBody] FinanciamentoViewModel financiamento)
        {
            return Ok(_financiamentoService.AdicionarFinanciamento(financiamento));

        }
    }
}
