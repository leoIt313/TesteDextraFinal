using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido> 
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido", "dbo");
            builder.HasKey(x => x.IdPedido);
            builder.Property(x => x.IdPedido).HasColumnName(@"IdPedido").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NumeroPedido).HasColumnName(@"NumeroPedido").HasColumnType("int").IsRequired();
            builder.Property(x => x.NomeLanche).HasColumnName(@"NomeLanche").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.DataPedido).HasColumnName(@"DataPedido").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.ValorFinal).HasColumnName(@"ValorFinal").HasColumnType("decimal").IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.IdStatusPedido).HasColumnName(@"IdStatusPedido").HasColumnType("int").IsRequired();
            builder.HasOne(a => a.StatusPedido).WithMany(b => b.Pedidoes).HasForeignKey(c => c.IdStatusPedido);
        }
    }
}
