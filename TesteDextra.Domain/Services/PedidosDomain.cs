using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteDextra.Domain.DomainModels;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Enum;
using TesteDextra.Domain.Interfaces.Repository;
using TesteDextra.Domain.Interfaces.Services;

namespace TesteDextra.Domain.Services
{
    public class PedidosDomain : IPedidosDomain
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly ILancheRepository _lancheRepository;
        private readonly IPedidoIngredienteRepository _pedidoIngredienteRepository;
        private readonly IParametroRepository _parametroRepository;

        public PedidosDomain(IPedidoRepository pedidoRepository,
                             IIngredienteRepository ingredienteRepository,
                             IUnitOfWork unitOfWork,
                             ILancheRepository lancheRepository,
                             IPedidoIngredienteRepository pedidoIngredienteRepository,
                             IParametroRepository parametroRepository)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
            _ingredienteRepository = ingredienteRepository;
            _lancheRepository = lancheRepository;
            _pedidoIngredienteRepository = pedidoIngredienteRepository;
            _parametroRepository = parametroRepository;
        }

        public IEnumerable<Pedido> GetAllPedidos()
        {
            return _pedidoRepository.GetAllPedidos();
        }

        public Pedido CadastrarPedido(Lanche lanche, List<ComplementoDomainModel> complementos)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                //Todo Aplicar regras para salvar Pedido
                var inflacao = Convert.ToDecimal(_parametroRepository.GetParametroById((long)ParametroEnum.Inflacao).Valor);
                Lanche lancheResult = _lancheRepository.GetLancheById(lanche.IdLanche);

                List<ComplementoDomainModel> ingredientesTotal = new List<ComplementoDomainModel>();

                ingredientesTotal.AddRange(lancheResult.LancheIngredientes.Select(x => new ComplementoDomainModel
                {
                    IdComplemento = x.Ingrediente.IdIngrediente,
                    Valor = (x.Ingrediente.Valor) + (x.Ingrediente.Valor * inflacao / 100),
                }));

                ingredientesTotal.AddRange(complementos.Where(x => x.Quantidade > 0));



                var resultPedido = _pedidoRepository.SavePedido(new Pedido
                {
                    DataPedido = DateTime.Now,
                    NumeroPedido = new Random().Next(1, 999),
                    IdStatusPedido = (int)StatusPedidoEnum.Efetuado,
                    ValorFinal = EfetuarSomaValorPedido(ingredientesTotal),
                    NomeLanche = lancheResult.Nome
                });


                foreach (var item in ingredientesTotal.ToList())
                {
                    _pedidoIngredienteRepository.SavePedidoIngrediente(new PedidoIngrediente
                    {
                        IdIngrediente = item.IdComplemento,
                        IdPedido = resultPedido.IdPedido
                    });
                }

                _unitOfWork.Commit();

                return resultPedido;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();

            }
            finally
            {
                Dispose();
            }

            return new Pedido();
        }

        public decimal SimularPedido(Lanche lanche, List<ComplementoDomainModel> complementos)
        {

            //Todo Aplicar regras para salvar Pedido
            var inflacao = Convert.ToDecimal(_parametroRepository.GetParametroById((long)ParametroEnum.Inflacao).Valor);
            Lanche lancheResult = _lancheRepository.GetLancheById(lanche.IdLanche);

            List<ComplementoDomainModel> ingredientesTotal = new List<ComplementoDomainModel>();

            ingredientesTotal.AddRange(lancheResult.LancheIngredientes.Select(x => new ComplementoDomainModel
            {
                IdComplemento = x.Ingrediente.IdIngrediente,
                Valor = (x.Ingrediente.Valor) + (x.Ingrediente.Valor * inflacao / 100),
            }));

            ingredientesTotal.AddRange(complementos.Where(x => x.Quantidade > 0));

            return EfetuarSomaValorPedido(ingredientesTotal); ;
        }

        private decimal EfetuarSomaValorPedido(IEnumerable<ComplementoDomainModel> listIngredientes)
        {
            decimal soma = 0;
            foreach (var item in listIngredientes.ToList())
            {
                if (item.Quantidade == decimal.Zero)
                {
                    soma += item.Valor;
                }
                else
                {
                    soma += item.Valor * item.Quantidade;
                }
            }

            return AplicarDesconto(listIngredientes, soma);
        }

        private decimal AplicarDesconto(IEnumerable<ComplementoDomainModel> listIngredientes, decimal valorTotal)
        {
            if (listIngredientes.Any(x => x.IdComplemento == (long)IngredienteEnum.Alface) && !listIngredientes.Any(x => x.IdComplemento == (long)IngredienteEnum.Bacon))
            {
                valorTotal = valorTotal - (valorTotal * 10 / 100);
                //Light
            }
            else
            {

                var quantidadeDescontarMuitaCarne = 0;
                listIngredientes.Where(x => x.IdComplemento == (long)IngredienteEnum.HamburguerCarne).ToList().ForEach(x => quantidadeDescontarMuitaCarne += (x.Quantidade == decimal.Zero) ? 1 : x.Quantidade);

                quantidadeDescontarMuitaCarne = quantidadeDescontarMuitaCarne / 3;

                if (quantidadeDescontarMuitaCarne > 0)
                {
                    valorTotal = valorTotal - (quantidadeDescontarMuitaCarne * listIngredientes.First(x => x.IdComplemento == (long)IngredienteEnum.HamburguerCarne).Valor);
                    //Muita carne
                }

                else
                {
                    var quantidadeDescontarMuitoQueijo = 0;
                    listIngredientes.Where(x => x.IdComplemento == (long)IngredienteEnum.Queijo).ToList().ForEach(x => quantidadeDescontarMuitoQueijo += (x.Quantidade == decimal.Zero) ? 1 : x.Quantidade);

                    quantidadeDescontarMuitoQueijo = quantidadeDescontarMuitoQueijo / 3;

                    if (quantidadeDescontarMuitoQueijo > 0)
                    {
                        valorTotal = valorTotal - (quantidadeDescontarMuitoQueijo * listIngredientes.First(x => x.IdComplemento == (long)IngredienteEnum.Queijo).Valor);
                        //Muito Queijo
                    }
                }
            }

            return valorTotal;
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
            _unitOfWork.Dispose();
            _ingredienteRepository.Dispose();
            _lancheRepository.Dispose();
            _pedidoIngredienteRepository.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
