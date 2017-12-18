using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Repository
{
    public interface IPedidoRepository:IDisposable
    {
        IEnumerable<Pedido> GetAllPedidos();

        Pedido SavePedido(Pedido pedido);

    }
}
