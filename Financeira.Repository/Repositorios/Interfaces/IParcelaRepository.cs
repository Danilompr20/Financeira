using Financeira.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Repositorios.Interfaces
{
    public interface IParcelaRepository
    {
        Task AdicionarParcela(Parcela parcela);
        Task UpdateParcela(Parcela parcela, int id);
    }
}
