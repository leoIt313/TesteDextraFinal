using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Mappings
{
    public class LancheMap :  IEntityTypeConfiguration<Lanche> 
    {
        public void Configure(EntityTypeBuilder<Lanche> builder)
        {
            builder.ToTable("Lanche", "dbo");
            builder.HasKey(x => x.IdLanche);
            builder.Property(x => x.IdLanche).HasColumnName(@"IdLanche").HasColumnType("bigint").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar(100)").IsRequired().IsUnicode(false);
        }
    }
}
