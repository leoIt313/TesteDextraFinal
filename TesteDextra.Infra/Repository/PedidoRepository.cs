using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(TesteDextraContext context) : base(context)
        {

        }

        public IEnumerable<Pedido> GetAllPedidos()
        {
            return this.GetAll().Include(x => x.PedidoIngredientes).Include(s => s.StatusPedido)
                                                                   .Include(x => x.PedidoIngredientes)
                                                                            .ThenInclude(y => y.Ingrediente)
                                                                            .ThenInclude(z => z.LancheIngredientes)
                                                                            .ThenInclude(w => w.Lanche).ToList();
        }

        public Pedido SavePedido(Pedido pedido)
        {
            this.Add(pedido);
            this.SaveChanges();
            return pedido;
        }
       
    }
}
