using System.Collections.Generic;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Services
{
    public interface IComplementosDomain
    {
        IEnumerable<Ingrediente> GetComplementosLanche();
    }
}
