using Financeira.Domain.Entity;
using Financeira.Repository.Context;
using Financeira.Repository.Repositorios.Interfaces;


namespace Financeira.Repository.Repositorios
{
    public class FinanciamentoRepository : IfinanciamentoRepository
    {

        protected readonly ContextoDB _context;

        public FinanciamentoRepository(ContextoDB contexto)
        {
            _context = contexto;
        }
        public  int AdicionarFinanciamento(Financiamento financiamento)
        {
            try
            {
                _context.Financiamentos.Add(financiamento);
                _context.SaveChanges();
                return financiamento.Id;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
             
        }
    }
}
