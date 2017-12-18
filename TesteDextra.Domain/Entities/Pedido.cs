using System;
using System.Collections.Generic;

namespace TesteDextra.Domain.Entities
{
    public class Pedido
    {
        public long IdPedido { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorFinal { get; set; }
        public int IdStatusPedido { get; set; }

        public virtual ICollection<PedidoIngrediente> PedidoIngredientes { get; set; }

        public virtual StatusPedido StatusPedido { get; set; }

        public string NomeLanche { get; set; }

        public Pedido()
        {
            PedidoIngredientes = new List<PedidoIngrediente>();
        }
    }
}
