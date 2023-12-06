using Financeira.Domain.Enum;


namespace Financeira.Domain.Entity
{
    public class Financiamento: BaseEntity
    {

        public Cliente Cliente { get; set; }
        public string CPF { get; set; }
        public TipoFinanciamento TipoFinanciamento { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataUltimoFinanciamento { get; set; }
        public List<Parcela> Parcelas { get; set; }
    }

}
