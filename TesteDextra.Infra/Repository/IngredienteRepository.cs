using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Infra.Context;

namespace TesteDextra.Infra.Repository
{
    public class IngredienteRepository : Repository<Ingrediente>, IComplementosRepository, IIngredienteRepository
    {
        public IngredienteRepository(TesteDextraContext context) : base(context)
        {
        }

        public IEnumerable<Ingrediente> GetComplementosLanche()
        {
            return GetAll().ToList();
        }

        public Ingrediente SaveIngrediente(Ingrediente ingrediente)
        {
            Add(ingrediente);
            SaveChanges();
            return ingrediente;
        }

        public IEnumerable<Ingrediente> GetIngredienteInId(List<Ingrediente> listIngredientes)
        {
            return Find(x => listIngredientes.Any(y => y.IdIngrediente == x.IdIngrediente));
        }
       
    }
}
