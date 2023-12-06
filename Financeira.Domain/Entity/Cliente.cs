

namespace Financeira.Domain.Entity
{
    public class Cliente : BaseEntity
    {
        
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string UF { get; set; }
        public string Celular { get; set; }
        public List<Financiamento> Financiamentos { get; set; }
    }
}
