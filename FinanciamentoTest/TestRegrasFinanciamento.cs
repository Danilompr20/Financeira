using Financeira.Domain.Entity;
using Financeira.Domain.Enum;
using Financeira.Domain.ViewModel;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service;
using Financeira.Service.ExceptionUtil;
using Moq;
using Xunit;

namespace FinanciamentoTest
{
    public class TestRegrasFinanciamento
    {
        [Fact]
        public void Criar_Financiamento_Aprovado()
        {
            var response = new ResponseViewModel();
            response.Status = "Aprovado";
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 20000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_PESSOA_JURIDICA,
                QuantidadeParcelas = 10,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };

            var financiamentoAprovado = new Financiamento()
            {
                Cliente = new Cliente(),
                CPF = financiamento.CPF,
                TipoFinanciamento = financiamento.Tipo,
                ValorTotal = financiamento.ValorCredito,
                DataUltimoFinanciamento = financiamento.DataPrimeiroVencimento,
                Parcelas = new List<Parcela>()
            };
            var validator = new FinanciamentoValidator();
            var mockFinancimentoRepository = new Mock<IfinanciamentoRepository>();
            mockFinancimentoRepository.Setup(x => x.AdicionarFinanciamento(financiamentoAprovado));
            var mockParcelaRepository = new Mock<IParcelaRepository>();
            var financiamentoService = new FinanciamentoService(mockFinancimentoRepository.Object, mockParcelaRepository.Object);
            //validator.Validate(financiamento);
            var result = financiamentoService.AdicionarFinanciamento(financiamento);
            Assert.Equal(result.Status,response.Status);
        }

        [Fact]
        public void Criar_Financiamento_Reprovado()
        {
            var response = new ResponseViewModel();
            response.Status = "Reprovado";
            var financiamento = new FinanciamentoViewModel()
            {
                ValorCredito = 20000,
                Tipo = Financeira.Domain.Enum.TipoFinanciamento.CREDITO_PESSOA_JURIDICA,
                QuantidadeParcelas = 4,
                DataPrimeiroVencimento = DateTime.Now.AddDays(16),
                CPF = "12345678"
            };

            var financiamentoAprovado = new Financiamento()
            {
                Cliente = new Cliente(),
                CPF = financiamento.CPF,
                TipoFinanciamento = financiamento.Tipo,
                ValorTotal = financiamento.ValorCredito,
                DataUltimoFinanciamento = financiamento.DataPrimeiroVencimento,
                Parcelas = new List<Parcela>()
            };
            var validator = new FinanciamentoValidator();
            var mockFinancimentoRepository = new Mock<IfinanciamentoRepository>();
            mockFinancimentoRepository.Setup(x => x.AdicionarFinanciamento(financiamentoAprovado)).Returns(1);
            var mockParcelaRepository = new Mock<IParcelaRepository>();
            var financiamentoService = new FinanciamentoService(mockFinancimentoRepository.Object, mockParcelaRepository.Object);
           
            
            Assert.Throws<ErroDeValidacaoException>(() => financiamentoService.AdicionarFinanciamento(financiamento));
        }
    }
}
