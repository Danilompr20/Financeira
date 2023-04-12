using Financeira.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.ConfigurationBuilder
{
    internal class FinanciamentoConfiguracao : IEntityTypeConfiguration<Financiamento>
    {
        public void Configure(EntityTypeBuilder<Financiamento> builder)
        {
            builder.HasOne(x => x.Cliente).WithMany(x => x.Financiamentos).HasPrincipalKey(x=> x.CPF).HasForeignKey(x => x.CPF);
            builder.HasMany(x => x.Parcelas).WithOne(x => x.Financiamento);
        }
    }
}
