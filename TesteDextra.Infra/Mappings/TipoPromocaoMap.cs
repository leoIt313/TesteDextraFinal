using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Infra.Mappings
{

    public class TipoPromocaoMap : IEntityTypeConfiguration<TipoPromocao>
    {
        public void Configure(EntityTypeBuilder<TipoPromocao> builder)
        {
            builder.ToTable("TipoPromocao", "dbo");
            builder.HasKey(x => x.IdTipoPromocao);

            builder.Property(x => x.IdTipoPromocao).HasColumnName(@"IdTipoPromocao").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar(30)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Descricao).HasColumnName(@"Descricao").HasColumnType("varchar(255)").IsUnicode(false);
        }
    }
}
