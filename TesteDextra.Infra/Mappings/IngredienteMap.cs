using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Mappings
{
    public class IngredienteMap :  IEntityTypeConfiguration<Ingrediente> 
    {
        public void Configure(EntityTypeBuilder<Ingrediente> builder)
        {
            builder.ToTable("Ingrediente", "dbo");
            builder.HasKey(x => x.IdIngrediente);

            builder.Property(x => x.IdIngrediente).HasColumnName(@"IdIngrediente").HasColumnType("bigint").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Valor).HasColumnName(@"Valor").HasColumnType("decimal").IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar(100)").IsRequired().IsUnicode(false);
        }
    }
}
