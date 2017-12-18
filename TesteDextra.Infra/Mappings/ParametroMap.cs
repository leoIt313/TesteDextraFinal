using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Infra.Mappings
{
    public class ParametroMap :  IEntityTypeConfiguration<Parametro> 
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.ToTable("Parametro", "dbo");
            builder.HasKey(x => x.IdParametro);
            builder.Property(x => x.IdParametro).HasColumnName(@"IdParametro").HasColumnType("bigint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar(50)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Valor).HasColumnName(@"Valor").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
        }
    }
}
