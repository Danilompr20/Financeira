using Financeira.Domain.Entity;
using Financeira.Domain.ViewModel;


namespace Financeira.Service.Interfaces
{
    public interface IParcelaService
    {
        Task AdicionarParcela(Parcela parcela);
        Task UpdateParcela(ParcelaViewModel parcela, int id);
    }
}
