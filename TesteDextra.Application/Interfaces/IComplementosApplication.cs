using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Application.ViewModel.Response;

namespace TesteDextra.Application.Interfaces
{
    public interface IComplementosApplication
    {
        IEnumerable<ComplementoViewModel> GetComplementosLanche();
    }
}
