using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDextra.Application.ViewModel.Response
{
    public class PedidosViewModel
    {
        public string Codigo { get; set; }
        public string Lanche { get; set; }
        public List<string> Complementos { get; set; }
        public string Valor { get; set; }
        public string DataHora { get; set; }
        public string Status { get; set; }
    }
}
