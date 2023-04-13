using Financeira.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Service.Interfaces
{
    public interface IParcelaService
    {
        Task AdicionarParcela(Parcela parcela);
    }
}
