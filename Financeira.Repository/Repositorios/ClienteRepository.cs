using Financeira.Domain.Entity;
using Financeira.Repository.Context;
using Financeira.Repository.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {

        protected readonly ContextoDB _context;

        public ClienteRepository(ContextoDB contexto)
        {
            _context = contexto;
        }
        public async Task AdicionarCliente(Cliente cliente)
        {
             _context.Add(cliente);
             _context.SaveChanges();
        }

        public async  Task<List<Cliente>> ListarClientesByEstado()
        {
            try
            {
                var cpf = _context.Clientes.Include(x => x.Financiamentos).ThenInclude(x => x.Parcelas)
                .Where(x => x.UF == "SP").Select(x=> x.CPF).ToList();
               
                var financiamento = _context.Financiamentos
                    .Include(x => x.Cliente)
                    .Include(x=> x.Parcelas).Where
                    (x => cpf.Contains(x.CPF) && x.Parcelas
                    .Where(p => p.IdFinanciamento == x.Id && p.DataPagamento != null).Count() >= (60f/100f) * x.Parcelas.Count())
                    .ToList().Select(x => new Cliente()
                    {
                        Celular = x.Cliente.Celular,
                        Nome = x.Cliente.Nome,
                        UF = x.Cliente.UF,
                        CPF = x.Cliente.CPF
                    }).ToList(); 

                return financiamento;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
          
        }
       
        public async Task<List<Cliente>> ListarClientesQuePossuemParcelasPagasSemAtraso()
        {
            try
            {
                var query = _context.Financiamentos.Include(x => x.Cliente).Where
               ( x => x.Parcelas.Any(p => p.DataVencimento > DateTime.Now.AddDays(5) && p.DataPagamento == null)).ToList()
               .Select(x => new Cliente()
               {
                   Celular = x.Cliente.Celular,
                   Nome = x.Cliente.Nome,
                   UF = x.Cliente.UF,
                   CPF = x.Cliente.CPF
               }).Take(4).ToList();

                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
