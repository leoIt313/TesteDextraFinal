using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Infra.Mappings
{
    public class PedidoIngredienteMap : IEntityTypeConfiguration<PedidoIngrediente>
    {
        public void Configure(EntityTypeBuilder<PedidoIngrediente> builder)
        {
            builder.ToTable("PedidoIngrediente", "dbo");
            builder.HasKey(x => x.IdPedidoIngrediente);
            builder.Property(x => x.IdPedidoIngrediente).HasColumnName(@"IdPedidoIngrediente").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IdPedido).HasColumnName(@"IdPedido").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.IdIngrediente).HasColumnName(@"IdIngrediente").HasColumnType("bigint").IsRequired();
            builder.HasOne(a => a.Ingrediente).WithMany(b => b.PedidoIngredientes).HasForeignKey(c => c.IdIngrediente);
            builder.HasOne(a => a.Pedido).WithMany(b => b.PedidoIngredientes).HasForeignKey(c => c.IdPedido);
        }
    }
}
