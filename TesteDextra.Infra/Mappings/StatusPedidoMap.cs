using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Infra.Mappings
{
    public class StatusPedidoMap : IEntityTypeConfiguration<StatusPedido>
    {
        public void Configure(EntityTypeBuilder<StatusPedido> builder)
        {
            builder.ToTable("StatusPedido", "dbo");
            builder.HasKey(x => x.IdStatusPedido);
            builder.Property(x => x.IdStatusPedido).HasColumnName(@"IdStatusPedido").HasColumnType("int").ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Sigla).HasColumnName(@"Sigla").HasColumnType("varchar(3)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Descricao).HasColumnName(@"Descricao").HasColumnType("varchar(15)").IsRequired().IsUnicode(false);
        }
    }
}
