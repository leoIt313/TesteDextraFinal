using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDextra.Domain.Entities
{
    public class Lanche
    {
        public long IdLanche { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<LancheIngrediente> LancheIngredientes { get; set; }

        public Lanche()
        {
            LancheIngredientes = new List<LancheIngrediente>();
        }
    }
}
