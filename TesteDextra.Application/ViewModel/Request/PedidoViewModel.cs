using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDextra.Application.ViewModel.Request
{
    public class PedidoViewModel
    {
        public long IdLancheSelecionado { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class ComplementoViewModel
    {
        public long IdComplemento { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }
    }
}
