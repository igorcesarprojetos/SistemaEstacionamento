using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaEstacionamento.Main.Models;
using SistemaEstacionamento.Main.Utilitarios.Helper.Interfaces;

namespace SistemaEstacionamento.Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao)
        {
            if (TempData != null)
            {
                TempData["Sucesso"] = null;
                TempData["Erro"] = null;
            }

            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null)
                return View();
            else
                return RedirectToAction(nameof(Index), "Autenticacao");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
