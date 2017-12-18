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
    public class LanchesApplication : ILanchesApplication
    {
        private readonly ILancheDomain _iLancheService;

        public LanchesApplication(ILancheDomain iLancheService)
        {
            _iLancheService = iLancheService;
        }

        public SelectLancheViewModel GetSelectLanches()
        {
            return Mapper.Map<IEnumerable<Lanche>, SelectLancheViewModel>(_iLancheService.GetAllLanches());
        }

        public LancheViewModel GetLancheById(long id)
        {
            return Mapper.Map<Lanche, LancheViewModel>(_iLancheService.GetLancheById(id));
        }
    }
}
