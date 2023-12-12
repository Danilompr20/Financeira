using Financeira.Domain.Entity;
using Financeira.Domain.Enum;
using Financeira.Domain.ViewModel;
using Financeira.Repository.Repositorios;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service.ExceptionUtil;
using Financeira.Service.Interfaces;
using System.Data;


namespace Financeira.Service
{
    public class FinanciamentoService : IFinanciamentoService
    {
        private readonly IParcelaRepository _parcelaRepository;
        private readonly IfinanciamentoRepository _financiamentoRepository;
        public FinanciamentoService(IfinanciamentoRepository financiamentoRepository, IParcelaRepository parcelaRepository)
        {
            _financiamentoRepository = financiamentoRepository;
            _parcelaRepository = parcelaRepository;
        }
        public ResponseViewModel AdicionarFinanciamento(FinanciamentoViewModel financiamento)
        {
          
                var response = new ResponseViewModel();
                var valorTotal = ValorCreditoComJuros(financiamento, out decimal juros);
                var validator = new FinanciamentoValidator();
                var result = validator.Validate(financiamento);
                if (!result.IsValid)
                {
                       response.Status = $"Reprovado {result.Errors.Select(x=> x.ErrorMessage).ToString()}";
                       response.ValorDoJuros = juros;
                       response.ValorComjuros = valorTotal;
                    throw new ErroDeValidacaoException(result.Errors.Select(x => x.ErrorMessage).ToList());
                }
               
                var financiamentoAprovado = new Financiamento();
                financiamentoAprovado.TipoFinanciamento = financiamento.Tipo;
                financiamentoAprovado.DataUltimoFinanciamento = financiamento.DataPrimeiroVencimento.AddMonths(financiamento.QuantidadeParcelas-1);
                financiamentoAprovado.CPF = financiamento.CPF;
                financiamentoAprovado.ValorTotal = valorTotal;
                salvarParcelas(financiamento, financiamentoAprovado);
                var idFinanciamento = _financiamentoRepository.AdicionarFinanciamento(financiamentoAprovado);
                financiamento.ValorCredito = valorTotal;
               

                response.Status = "Aprovado";
                response.ValorDoJuros = juros;
                response.ValorComjuros = valorTotal;
                return response;
            
        }
        private void salvarParcelas(FinanciamentoViewModel financiamento,   Financiamento fin)
        {
            fin.Parcelas = new List<Parcela>();
            for (int i = 0; i < financiamento.QuantidadeParcelas;i++)
            {
                var parcelas = new Parcela();

               
                parcelas.NumeroDeParcela = i + 1;
                parcelas.Valor = financiamento.ValorCredito / financiamento.QuantidadeParcelas;
                parcelas.DataPagamento = null;
                if (i > 0)
                {
                    parcelas.DataVencimento =  financiamento.DataPrimeiroVencimento.AddMonths(i);
                   
                }
                else
                {
                    parcelas.DataVencimento = financiamento.DataPrimeiroVencimento;
                   
                }
               
                fin.Parcelas.Add(parcelas);
            }
        }
       
        public decimal ValorCreditoComJuros(FinanciamentoViewModel financiamento, out decimal valorJuros)
        {
            decimal valorComjuros = 0;
            valorJuros = 0;
            switch (financiamento.Tipo)
            {
                case TipoFinanciamento.CREDITO_DIRETO:
                    valorJuros = financiamento.ValorCredito * 2 / 100;
                    valorComjuros = financiamento.ValorCredito + valorJuros;
                    break;
                case TipoFinanciamento.CREDITO_CONSIGNADO:
                    valorJuros = financiamento.ValorCredito * 1 / 100;
                    valorComjuros = financiamento.ValorCredito + valorJuros;
                    break;
                case TipoFinanciamento.CREDITO_PESSOA_JURIDICA:
                    valorJuros = financiamento.ValorCredito * 5 / 100;
                    valorComjuros = financiamento.ValorCredito + valorJuros;
                    break;
                case TipoFinanciamento.CREDITO_PESSOA_FISICA:
                    valorJuros = financiamento.ValorCredito * 3 / 100;
                    valorComjuros = financiamento.ValorCredito + valorJuros;
                    break;
                case TipoFinanciamento.CREDITO_IMOBILIARIO:
                    valorJuros = financiamento.ValorCredito * 9 / 100;
                    valorComjuros = financiamento.ValorCredito + valorJuros;
                    break;
                default:
                    break;
            }
            return valorComjuros;
        }
    }
}
