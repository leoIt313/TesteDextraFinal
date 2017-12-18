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
    public class LancheDomain : ILancheDomain, ILancheRepository
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly IParametroRepository _parametroRepository;

        public LancheDomain(ILancheRepository lancheRepository, IParametroRepository parametroRepository)
        {
            _lancheRepository = lancheRepository;
            _parametroRepository = parametroRepository;
        }

        public IEnumerable<Lanche> GetAllLanches()
        {
           return _lancheRepository.GetAllLanches();
        }

        public Lanche GetLancheById(long id)
        {
            var inflacao = Convert.ToDecimal(_parametroRepository.GetParametroById((long)ParametroEnum.Inflacao).Valor);

            var lanche = _lancheRepository.GetLancheById(id);

            lanche.LancheIngredientes = lanche.LancheIngredientes.Select(x=> new LancheIngrediente
            {
                Ingrediente = new Ingrediente
                {
                    IdIngrediente = x.Ingrediente.IdIngrediente,
                    Nome = x.Ingrediente.Nome,
                    LancheIngredientes = x.Lanche.LancheIngredientes,
                    Valor = (x.Ingrediente.Valor) + (x.Ingrediente.Valor * inflacao / 100),
                    PedidoIngredientes = x.Ingrediente.PedidoIngredientes
                },
                IdLanche = x.IdLanche,
                IdIngrediente = x.IdIngrediente,
                Lanche = x.Lanche,
                IdLancheIngrediente = x.IdIngrediente
            }).ToList();

            return lanche;
        }

        public void Dispose()
        {
            _lancheRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
