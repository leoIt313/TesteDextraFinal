
namespace TesteDextra.Domain.Entities
{
    public class LancheIngrediente
    {
        public long IdLancheIngrediente { get; set; }
        public long IdLanche { get; set; }
        public long IdIngrediente { get; set; }

        public virtual Ingrediente Ingrediente { get; set; }

        public virtual Lanche Lanche { get; set; }
    }
}
