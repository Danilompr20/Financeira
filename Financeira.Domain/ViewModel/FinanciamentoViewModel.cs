using Financeira.Domain.Enum;


namespace Financeira.Domain.ViewModel
{
    public class FinanciamentoViewModel
    {
        public decimal ValorCredito { get; set; }
        public TipoFinanciamento Tipo { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
        public string CPF { get; set; }
    }
}
