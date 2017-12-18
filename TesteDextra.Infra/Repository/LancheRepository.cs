using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Repository
{
    public class LancheRepository : Repository<Lanche>, ILancheRepository
    {
        public LancheRepository(TesteDextraContext context) : base(context)
        {
        }

        public IEnumerable<Lanche> GetAllLanches()
        {
            return GetAll().Include(x => x.LancheIngredientes).ThenInclude(y => y.Ingrediente).ToList();
        }
        public Lanche GetLancheById(long id)
        {
            return GetAll().Include(x=>x.LancheIngredientes).ThenInclude(y=>y.Ingrediente).First(w=>w.IdLanche.Equals(id));
        }
        public Lanche GetLancheByName(string nome)
        {
            return GetAll().Include(x => x.LancheIngredientes).ThenInclude(y => y.Ingrediente).First(w => w.Nome.Equals(nome));
        }
    }
}
