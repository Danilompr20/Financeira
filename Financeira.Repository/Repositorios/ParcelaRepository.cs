using Financeira.Domain.Entity;
using Financeira.Repository.Context;
using Financeira.Repository.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Repositorios
{
    public class ParcelaRepository : IParcelaRepository
    {
        protected readonly ContextoDB _context;

        public ParcelaRepository(ContextoDB contexto)
        {
            _context = contexto;
        }
        public async Task AdicionarParcela(Parcela parcela)
        {
            try
            {
                _context.Add(parcela);
                _context.SaveChanges();
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
