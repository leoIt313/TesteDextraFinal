using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Repository
{
    public class ParametroRepository : Repository<Parametro>, IParametroRepository
    {
       
        public ParametroRepository(TesteDextraContext context) : base(context)
        {
        }

        public Parametro GetParametroById(long id)
        {
            return this.GetById(id);
        }
    }
}
