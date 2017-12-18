using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteDextra.Application.Interfaces;
using TesteDextra.Application.ViewModel.Request;
using Microsoft.AspNetCore.Http;
using TesteDextra.MVC.Models;

namespace TesteDextra.MVC.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILanchesApplication _lanches;
        private readonly IComplementosApplication _complementosApplication;
        private readonly IPedidosApplication _pedidosApplication;

        public PedidosController(ILanchesApplication lanches, IComplementosApplication complementosApplication, IPedidosApplication pedidosApplication)
        {
            _lanches = lanches;
            _complementosApplication = complementosApplication;
            _pedidosApplication = pedidosApplication;

        }

        public IActionResult Index()
        {
            return View(_lanches.GetSelectLanches());
        }

        public PartialViewResult GetDescricaoLanche(long id)
        {
            return PartialView("_DescricaoLanche", _lanches.GetLancheById(id));
        }

        public PartialViewResult GetComplementosLanche()
        {
            return PartialView("_ItensComplemento", _complementosApplication.GetComplementosLanche().ToList());
        }

        public object Cadastrar(PedidoViewModel pedidoViewModel, List<ComplementoViewModel> complementoViewModel)
        {
            try
            {
                var result = _pedidosApplication.CadastrarPedido(pedidoViewModel, complementoViewModel);

                return new ExecutionResult(StatusCodes.Status200OK, string.Format("O seu pedido no valor de {0} foi efetuado com sucesso", result.ValorTotal.ToString("C")), result);
            }
            catch (ArgumentException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new ExecutionResult(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new ExecutionResult(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        public JsonResult SimularPedido(PedidoViewModel pedidoViewModel, List<ComplementoViewModel> complementoViewModel)
        {
            return Json(_pedidosApplication.SimularPedido(pedidoViewModel, complementoViewModel));
        }

    }
}