using Financeira.Domain.Entity;
using Financeira.Repository.ConfigurationBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.Context
{
    public class ContextoDB : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Financiamento> Financiamentos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }

        public ContextoDB(DbContextOptions<ContextoDB> options)
          : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParcelaConfiguracao());
            modelBuilder.ApplyConfiguration(new FinanciamentoConfiguracao());
            modelBuilder.ApplyConfiguration(new ClienteConfiguracao());
        }
    }
}
