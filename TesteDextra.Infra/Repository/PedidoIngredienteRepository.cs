using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Repository
{
    public class PedidoIngredienteRepository : Repository<PedidoIngrediente>, IPedidoIngredienteRepository
    {
        public PedidoIngredienteRepository(TesteDextraContext context) : base(context)
        {
        }

        public PedidoIngrediente SavePedidoIngrediente(PedidoIngrediente pedidoIngrediente)
        {
            this.Add(pedidoIngrediente);
            this.SaveChanges();
            return pedidoIngrediente;
        }
    }
}
