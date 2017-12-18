using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using TesteDextra.Application.ViewModel.Request;
using TesteDextra.Application.ViewModel.Response;
using TesteDextra.Domain.Entities;
using ComplementoViewModel = TesteDextra.Application.ViewModel.Response.ComplementoViewModel;

namespace TesteDextra.Application.Automapper.DomainToViewModel
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            this.Configure();
        }

        protected void Configure()
        {
            CreateMap<Pedido, PedidosViewModel>().ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.NumeroPedido))
                                                 .ForMember(dest => dest.Complementos, opt => opt.MapFrom(src => src.PedidoIngredientes.Select(x => x.Ingrediente.Nome)))
                                                 .ForMember(dest => dest.DataHora, opt => opt.MapFrom(src => src.DataPedido))
                                                 .ForMember(dest => dest.Lanche, opt => opt.MapFrom(src => src.NomeLanche))
                                                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusPedido.Descricao))
                                                 .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.ValorFinal));


            CreateMap<IEnumerable<Lanche>, SelectLancheViewModel>().ForMember(dest => dest.ItemLancheViewModel, opt => opt.MapFrom(src => src.Select(x => new ItemLancheViewModel { IdLanche = x.IdLanche, Nome = x.Nome })));


            CreateMap<Lanche, LancheViewModel>().ForMember(dest => dest.IdLanche, opt => opt.MapFrom(src => src.IdLanche))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Ingredientes, opt => opt.MapFrom(src => src.LancheIngredientes.Select(x => x.Ingrediente.Nome)))
                .ForMember(dest =>  dest.Valor, opt => opt.MapFrom(src => src.LancheIngredientes.Sum(x => x.Ingrediente.Valor)));

            CreateMap<Ingrediente, ComplementoViewModel>().ForMember(dest => dest.IdComplemento, opt => opt.MapFrom(src => src.IdIngrediente))
                                                          .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                                                          .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));

            CreateMap<Pedido, PedidoViewModel>().ForMember(dest => dest.IdLancheSelecionado, opt => opt.MapFrom(src => src.IdPedido))
                                                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorFinal));

        }
    }
}
