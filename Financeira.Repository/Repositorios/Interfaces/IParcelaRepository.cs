using Financeira.Domain.Entity;


namespace Financeira.Repository.Repositorios.Interfaces
{
    public interface IParcelaRepository
    {
        Task AdicionarParcela(Parcela parcela);
        Task UpdateParcela(Parcela parcela, int id);
    }
}
