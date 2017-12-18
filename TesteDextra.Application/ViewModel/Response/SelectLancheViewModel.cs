using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteDextra.Application.ViewModel.Response
{
    public class SelectLancheViewModel
    {
        public long IdLancheSelecionado { get; set; }
        public List<ItemLancheViewModel> ItemLancheViewModel { get; set; }
        public IEnumerable<SelectListItem> SelectItemLanche
        {
            get { return ItemLancheViewModel == null ? null : ItemLancheViewModel.Select(a => new SelectListItem() { Text = a.Nome, Value = a.IdLanche.ToString() }).OrderBy(x => x.Text).ToList(); }
        }
    }

    public class ItemLancheViewModel
    {
        public long IdLanche { get; set; }
        public string Nome { get; set; }
    }

}
