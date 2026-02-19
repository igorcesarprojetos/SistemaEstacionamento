using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SistemaEstacionamento.Main.Data;
using SistemaEstacionamento.Main.Models;
using SistemaEstacionamento.Main.Utilitarios.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Components.NavigationManager;

namespace SistemaEstacionamento.Main.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly SistemaEstacionamentoContext _context;

        public AutenticacaoController(SistemaEstacionamentoContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Login()
        {
            return View();
        }
        

        
        [HttpPost, ActionName("Logar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(string login, string senha)
        {
            try
            {
                var senhaEncrypt = HelperSHA256.Encrypt(senha);
                var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Login == login && x.Senha == senhaEncrypt);
                
                if (usuario == null)
                {
                    return NotFound();
                }

                return Redirect("/");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }             

        }
    }
}
