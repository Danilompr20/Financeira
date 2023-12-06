
using Financeira.Domain.ViewModel;


namespace Financeira.Service.Interfaces
{
    public interface IFinanciamentoService
    {
        ResponseViewModel AdicionarFinanciamento(FinanciamentoViewModel financiamento);
    }
}
