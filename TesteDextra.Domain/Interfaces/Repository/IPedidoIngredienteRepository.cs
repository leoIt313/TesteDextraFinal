using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Repository
{
    public interface IPedidoIngredienteRepository : IDisposable
    {
        PedidoIngrediente SavePedidoIngrediente(PedidoIngrediente pedidoIngrediente);
    }
}
