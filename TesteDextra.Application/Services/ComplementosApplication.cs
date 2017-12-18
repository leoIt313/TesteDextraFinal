using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TesteDextra.Application.Interfaces;
using TesteDextra.Application.ViewModel.Response;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Interfaces.Services;

namespace TesteDextra.Application.Services
{
    public class ComplementosApplication : IComplementosApplication
    {
        private readonly IComplementosDomain _complementosService;

        public ComplementosApplication(IComplementosDomain complementosService)
        {
            _complementosService = complementosService;
        }

        public IEnumerable<ComplementoViewModel> GetComplementosLanche()
        {
            return Mapper.Map<IEnumerable<Ingrediente>, List<ComplementoViewModel>>(_complementosService.GetComplementosLanche());
        }
    }
}
