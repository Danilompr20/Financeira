using Financeira.Domain.ViewModel;
using Financeira.Service.ExceptionUtil;
using Xunit;

namespace FinanciamentoTest
{
    public  class ValidatorTesteErro
    {
        [Fact]
        public void Validar_Sucesso()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 20000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_PESSOA_JURIDICA,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validar_Data_Vencimento_Maior_Que_Hoje()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 5000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now,
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }
        [Fact]
        public void Validar_Data_Vencimento_15Dias_Maior_Que_A_Data_de_Hoje()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 5000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(5),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validar_Data_Vencimento_40Dias_Maior_Que_A_Data_de_Hoje()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 5000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(45),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validar_Qtd_MinimaParcela_Maior_Que_5()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 5000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 4,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validar_Qtd_MaximaParcela_Menor_Que_72()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 5000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 73,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validar_ValorMaximo_Extrapolado()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 100000001,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_CONSIGNADO,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validar_ValorMinimo_PessoaJuridica()
        {
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 14999,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_PESSOA_JURIDICA,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };
            var validator = new FinanciamentoValidator();
            var result = validator.Validate(financiamento);

            Assert.False(result.IsValid);
        }
    }
}
