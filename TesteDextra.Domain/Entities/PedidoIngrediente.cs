
namespace TesteDextra.Domain.Entities
{
    public class PedidoIngrediente
    {
        public long IdPedidoIngrediente { get; set; } 
        public long IdPedido { get; set; } 
        public long IdIngrediente { get; set; } 
        public virtual Ingrediente Ingrediente { get; set; } 
        public virtual Pedido Pedido { get; set; } 
    }
}
