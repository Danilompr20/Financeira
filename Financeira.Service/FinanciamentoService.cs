using Financeira.Domain.Entity;
using Financeira.Domain.Enum;
using Financeira.Domain.ViewModel;
using Financeira.Repository.Repositorios;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var response = new ResponseViewModel();
                var valorTotal = valorCreditoComJuros(financiamento, out decimal juros);
                if (!validacoes(financiamento).Item1)
                {
                    response.Status = $"Reprovado - {validacoes(financiamento).Item2}";
                    response.ValorDoJuros = juros;
                    response.ValorComjuros = valorTotal;
                    return response;
                }
                var financiamentoAprovado = new Financiamento();
                financiamentoAprovado.TipoFinanciamento = financiamento.Tipo;
                financiamentoAprovado.DataUltimoFinanciamento = financiamento.DataPrimeiroVencimento.AddMonths(financiamento.QuantidadeParcelas-1);
                financiamentoAprovado.CPF = financiamento.CPF;
                financiamentoAprovado.ValorTotal = valorTotal;
                var idFinanciamento = _financiamentoRepository.AdicionarFinanciamento(financiamentoAprovado);
                financiamento.ValorCredito = valorTotal;
                salvarParcelas(financiamento, idFinanciamento);

                response.Status = "Aprovado";
                response.ValorDoJuros = juros;
                response.ValorComjuros = valorTotal;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void salvarParcelas(FinanciamentoViewModel financiamento, int idFinanciamento)
        {
            for (int i = 0; i < financiamento.QuantidadeParcelas;i++)
            {
                var parcelas = new Parcela();

                parcelas.IdFinanciamento = idFinanciamento;
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
                _parcelaRepository.AdicionarParcela(parcelas);
            }
        }
        private (bool, string) validacoes(FinanciamentoViewModel financiamento)
        {
            var mensagem = "";
            if (financiamento.DataPrimeiroVencimento < DateTime.Now)
            {
                mensagem = "Data do vencimento precisa ser maior que a data de Hoje";
                return (false, mensagem);
            }
            if (financiamento.DataPrimeiroVencimento < DateTime.Now.Date.AddDays(15) || financiamento.DataPrimeiroVencimento > DateTime.Now.Date.AddDays(40))
            {
                mensagem = "Data fora do permitido";
                return (false, mensagem);
            }
            if (financiamento.QuantidadeParcelas > 72 || financiamento.QuantidadeParcelas < 5)
            {
                mensagem = "Número máximo de parcelas é 72 e o mínimo é 5 ";
                return (false, mensagem);
            }

            if (financiamento.ValorCredito > 100000000)
            {
                mensagem = "valor máximo do empréstimo foi extrapolado";
                return (false, mensagem);
            }
            if (financiamento.Tipo.Equals( TipoFinanciamento.CREDITO_PESSOA_JURIDICA) && financiamento.ValorCredito < 15000)
            {
                mensagem = "Para Pessoa Jurídica o valor Mínimo de empréstimo é R$ 15.000,00";
                return (false, mensagem);
            }
            return (true, mensagem);
        }
        private decimal valorCreditoComJuros(FinanciamentoViewModel financiamento, out decimal valorJuros)
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
