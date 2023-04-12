using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Domain.Entity
{
    public class Parcela: BaseEntity
    {
        public int NumeroDeParcela { get; set; }
        public Financiamento Financiamento { get; set; }
        public int IdFinanciamento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
