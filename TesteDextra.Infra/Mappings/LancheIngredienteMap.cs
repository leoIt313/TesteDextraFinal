using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Mappings
{
    public class LancheIngredienteMap : IEntityTypeConfiguration<LancheIngrediente>  
    {
        public void Configure(EntityTypeBuilder<LancheIngrediente> builder)
        {
            builder.ToTable("LancheIngrediente", "dbo");
            builder.HasKey(x => x.IdLancheIngrediente);

            builder.Property(x => x.IdLancheIngrediente).HasColumnName(@"IdLancheIngrediente").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IdLanche).HasColumnName(@"IdLanche").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.IdIngrediente).HasColumnName(@"IdIngrediente").HasColumnType("bigint").IsRequired();

            builder.HasOne(a => a.Ingrediente).WithMany(b => b.LancheIngredientes).HasForeignKey(c => c.IdIngrediente);
            builder.HasOne(a => a.Lanche).WithMany(b => b.LancheIngredientes).HasForeignKey(c => c.IdLanche);
        }
    }
}
