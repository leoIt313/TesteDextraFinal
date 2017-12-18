using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Application.ViewModel.Request;
using TesteDextra.Application.ViewModel.Response;

namespace TesteDextra.Application.Interfaces
{
    public interface IPedidosApplication : IDisposable
    {
        IEnumerable<PedidosViewModel> GetAllPedidos();
        PedidoViewModel CadastrarPedido(PedidoViewModel pedido, List<ViewModel.Request.ComplementoViewModel> complementos);
        decimal SimularPedido(PedidoViewModel pedido, List<ViewModel.Request.ComplementoViewModel> complementos);
    }
}
