using Financeira.Domain.Entity;
using Financeira.Repository.Repositorios.Interfaces;
using Financeira.Service.Interfaces;
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
    }
}
