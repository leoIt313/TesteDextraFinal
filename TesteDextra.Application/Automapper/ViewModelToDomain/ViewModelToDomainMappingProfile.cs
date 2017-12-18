using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TesteDextra.Application.ViewModel.Request;
using TesteDextra.Application.ViewModel.Response;
using TesteDextra.Domain.DomainModels;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Application.Automapper.ViewModelToDomain
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            this.Configure();
        }

        protected void Configure()
        {
            CreateMap<PedidoViewModel, Lanche>().ForMember(dest => dest.IdLanche, opt => opt.MapFrom(src => src.IdLancheSelecionado));
            CreateMap<ViewModel.Request.ComplementoViewModel, ComplementoDomainModel>().ForMember(dest => dest.IdComplemento, opt => opt.MapFrom(src => src.IdComplemento))
                                                                                       .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                                                                                       .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));
        }
    }
}
