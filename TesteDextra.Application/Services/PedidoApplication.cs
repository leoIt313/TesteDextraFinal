using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TesteDextra.Application.Interfaces;
using TesteDextra.Application.ViewModel.Request;
using TesteDextra.Application.ViewModel.Response;
using TesteDextra.Domain.DomainModels;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Services;
using ComplementoViewModel = TesteDextra.Application.ViewModel.Request.ComplementoViewModel;

namespace TesteDextra.Application.Services
{
    public class PedidoApplication : IPedidosApplication
    {
        private readonly IPedidosDomain _iPedidosService;

        public PedidoApplication(IPedidosDomain iPedidosService)
        {
            _iPedidosService = iPedidosService;
        }

        public IEnumerable<PedidosViewModel> GetAllPedidos()
        {
            var teste = _iPedidosService.GetAllPedidos();
            return Mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidosViewModel>>(_iPedidosService.GetAllPedidos());
        }

        public PedidoViewModel CadastrarPedido(PedidoViewModel pedido, List<ViewModel.Request.ComplementoViewModel> complementos)
        {
            var mapLanche = Mapper.Map<PedidoViewModel, Lanche>(pedido);

            var mapComplementos = Mapper.Map<List<ViewModel.Request.ComplementoViewModel>, List<ComplementoDomainModel>>(complementos);

            return Mapper.Map<Pedido, PedidoViewModel>(_iPedidosService.CadastrarPedido(mapLanche, mapComplementos));
            
        }

        public decimal SimularPedido(PedidoViewModel pedido, List<ComplementoViewModel> complementos)
        {
            var mapLanche = Mapper.Map<PedidoViewModel, Lanche>(pedido);

            var mapComplementos = Mapper.Map<List<ViewModel.Request.ComplementoViewModel>, List<ComplementoDomainModel>>(complementos);

            return _iPedidosService.SimularPedido(mapLanche, mapComplementos);
        }

        public void Dispose()
        {
            _iPedidosService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
