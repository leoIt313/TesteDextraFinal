using System;
using System.Collections.Generic;
using System.Text;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Domain.Interfaces.Repository
{
    public interface IIngredienteRepository:IDisposable
    {
        Ingrediente SaveIngrediente(Ingrediente ingrediente);
        IEnumerable<Ingrediente> GetIngredienteInId(List<Ingrediente> listIngredientes);

    }
}
