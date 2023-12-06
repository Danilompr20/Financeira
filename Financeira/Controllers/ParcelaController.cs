using Financeira.Domain.ViewModel;
using Financeira.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Financeira.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaService _parcelaService;
        public ParcelaController(IFinanciamentoService financiamentoService, IParcelaService parcelaService)
        {
            _parcelaService = parcelaService;
        }

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> Adicionar([FromBody] ParcelaViewModel parcela, int id)
        {

            return Ok(_parcelaService.UpdateParcela(parcela,id));

        }
    }
}
