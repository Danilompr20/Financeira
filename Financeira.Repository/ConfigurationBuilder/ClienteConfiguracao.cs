using Financeira.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Repository.ConfigurationBuilder
{
    public class ClienteConfiguracao : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasMany(g => g.Financiamentos).WithOne(g => g.Cliente);
            builder.Property(x=> x.CPF).IsRequired();
            builder.HasIndex(x => x.CPF).IsUnique();
        }
    }
}
