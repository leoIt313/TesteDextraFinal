using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.DomainModels;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Services
{
    public interface IPedidosDomain : IDisposable
    {
        IEnumerable<Pedido> GetAllPedidos();

        Pedido CadastrarPedido(Lanche pedido, List<ComplementoDomainModel> complementos);
        decimal SimularPedido(Lanche lanche, List<ComplementoDomainModel> complementos);

    }
}
