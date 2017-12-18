using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDextra.Domain.Entities
{
    public class StatusPedido
    {
        public int IdStatusPedido { get; set; }
        public string Sigla { get; set; } 
        public string Descricao { get; set; }

        public virtual ICollection<Pedido> Pedidoes { get; set; }

        public StatusPedido()
        {
            Pedidoes = new List<Pedido>();
        }
    }
}
