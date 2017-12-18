using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Enum;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Domain.Interfaces.Services;

namespace TesteDextra.Domain.Services
{
    public class ComplementoDomain : IComplementosDomain
    {
        private readonly IComplementosRepository _complementosRepository;
        private readonly IParametroRepository _parametroRepository;

        public ComplementoDomain(IComplementosRepository complementosRepository, IParametroRepository parametroRepository)
        {
            _complementosRepository = complementosRepository;
            _parametroRepository = parametroRepository;
        }

        public IEnumerable<Ingrediente> GetComplementosLanche()
        {
            var inflacao = Convert.ToDecimal(_parametroRepository.GetParametroById((long)ParametroEnum.Inflacao).Valor);
            var ingredientes = _complementosRepository.GetComplementosLanche().Select(x => new Ingrediente
            {
                IdIngrediente = x.IdIngrediente,
                Valor = (x.Valor) + (x.Valor * inflacao / 100),
                LancheIngredientes = x.LancheIngredientes,
                Nome = x.Nome,
                PedidoIngredientes = x.PedidoIngredientes
            });
            return ingredientes;
        }
    }
}
