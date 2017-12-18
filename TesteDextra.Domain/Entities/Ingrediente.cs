using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDextra.Domain.Entities
{
    public class Ingrediente
    {
        public long IdIngrediente { get; set; } 
        public decimal Valor { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<LancheIngrediente> LancheIngredientes { get; set; } 
        public virtual ICollection<PedidoIngrediente> PedidoIngredientes { get; set; }


        public Ingrediente()
        {
            LancheIngredientes = new List<LancheIngrediente>();
            PedidoIngredientes = new List<PedidoIngrediente>();
        }
    }
}
