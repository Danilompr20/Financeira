using Financeira.Domain.Entity;
using Financeira.Domain.ViewModel;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Service
{
    public class ParcelaService : IParcelaService
    {
        private readonly IParcelaRepository _parcelaRepository;
        public ParcelaService(IParcelaRepository parcelaRepository)
        {
            _parcelaRepository = parcelaRepository;
        }
        public async Task AdicionarParcela(Parcela parcela)
        {
            await _parcelaRepository.AdicionarParcela(parcela);
        }

        public async Task UpdateParcela(ParcelaViewModel parcela, int id)
        {
            var entity = new Parcela()
            {
                Id = id,
                DataPagamento = parcela.DataPagamento,
                IdFinanciamento = parcela.IdFinanciamento
            };
            await _parcelaRepository.UpdateParcela(entity, id);
        }
        
    }
}
