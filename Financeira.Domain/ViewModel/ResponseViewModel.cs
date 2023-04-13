using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Domain.ViewModel
{
    public class ResponseViewModel
    {
        public string Status { get; set; }
        public decimal ValorComjuros { get; set; }
        public decimal ValorDoJuros { get; set; }
    }
}
