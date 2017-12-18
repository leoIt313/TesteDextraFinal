using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TesteDextra.Application.Interfaces;
using TesteDextra.MVC.Models;

namespace TesteDextra.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPedidosApplication _pedidosApplication;

        public HomeController(IPedidosApplication pedidosApplication)
        {
            _pedidosApplication = pedidosApplication;
        }

        public IActionResult Index()
        {
            return View(_pedidosApplication.GetAllPedidos());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
