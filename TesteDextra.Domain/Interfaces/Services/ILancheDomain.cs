using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Services
{
    public interface ILancheDomain
    {
        IEnumerable<Lanche> GetAllLanches();
      
        Lanche GetLancheById(long id);
    }
}
