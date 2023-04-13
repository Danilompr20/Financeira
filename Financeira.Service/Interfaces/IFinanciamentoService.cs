using Financeira.Domain.Entity;
using Financeira.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Service.Interfaces
{
    public interface IFinanciamentoService
    {
        ResponseViewModel AdicionarFinanciamento(FinanciamentoViewModel financiamento);
    }
}
