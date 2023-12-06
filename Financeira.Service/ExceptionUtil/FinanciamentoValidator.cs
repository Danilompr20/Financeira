using Financeira.Domain.Enum;
using Financeira.Domain.ViewModel;
using FluentValidation;


namespace Financeira.Service.ExceptionUtil
{
    public class FinanciamentoValidator : AbstractValidator<FinanciamentoViewModel>
    {
        public FinanciamentoValidator()
        {
            RuleFor(f => f.DataPrimeiroVencimento).Must(DataPrimeirovencimento).WithMessage("Data do vencimento precisa ser maior que a data de Hoje");
            RuleFor(f => f.DataPrimeiroVencimento).Must(DataForaPerimitido).WithMessage("Data fora do permitido");
            RuleFor(f => f.QuantidadeParcelas).Must(NumeroParcelasIncorretas).WithMessage("Número máximo de parcelas é 72 e o mínimo é 5");
            RuleFor(f => f.ValorCredito).Must(ValorCreditoExtrapolado).WithMessage("Valor máximo do empréstimo foi extrapolado");
            When(f => f.Tipo.Equals(TipoFinanciamento.CREDITO_PESSOA_JURIDICA), () =>
            {
                RuleFor(f => f.ValorCredito).Must(ValorMinimo).WithMessage("Para Pessoa Jurídica o valor Mínimo de empréstimo é R$ 15.000,00");

            });

        }

        private bool DataPrimeirovencimento(DateTime dataPrimeiroVencimento)
        {
            return dataPrimeiroVencimento > DateTime.Now;
        }
        private bool DataForaPerimitido(DateTime dataPrimeiroVencimento)
        {
            if (dataPrimeiroVencimento < DateTime.Now.Date.AddDays(15) || (dataPrimeiroVencimento > DateTime.Now.Date.AddDays(40)))
                return false;
            return true;

        }
        private bool NumeroParcelasIncorretas(int qtdParcelas)
        {
            if (qtdParcelas > 72 || qtdParcelas < 5)
                return false;
            return true;


        }
        private bool ValorCreditoExtrapolado(decimal valorCredito)
        {
            return valorCredito < 100000000;
        }
        public bool ValorMinimo(decimal valorMinimo)
        {
            return valorMinimo > 15000;

        }
    }
}
