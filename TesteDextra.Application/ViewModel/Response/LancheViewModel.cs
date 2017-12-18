using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteDextra.Application.ViewModel.Response
{
    public class LancheViewModel
    {
        public long IdLanche { get; set; }
        public string Nome { get; set; }
        public List<string> Ingredientes { get; set; }
        public string Valor { get; set; }

    }
}
