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
    public class ParcelaConfiguracao : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasOne(x => x.Financiamento).WithMany(x => x.Parcelas).HasForeignKey(x => x.IdFinanciamento);
        }
    }
}
