using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SistemaEstacionamento.Main.Data;
using SistemaEstacionamento.Main.Models;
using SistemaEstacionamento.Main.Utilitarios.Helper;
using SistemaEstacionamento.Main.Utilitarios.Helper.Interfaces;
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
        private readonly ISessao _sessao;

        public AutenticacaoController(SistemaEstacionamentoContext context,ISessao sessao)
        {
            _context = context;
            _sessao = sessao;
        }
        
        public async Task<IActionResult> Index()
        {
            if(_sessao.BuscarSessaoUsuario()!=null) return RedirectToAction(nameof(Index), "Home");

            return View();
        }
                
        [HttpPost]
        public async Task<IActionResult> Logar(string login, string senha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senhaEncrypt = HelperSHA256.Encrypt(senha);
                    var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Login == login && x.Senha == senhaEncrypt);
                    
                    if (usuario != null)
                    {
                        if (!usuario.IndAtivo)
                            TempData["MensagemErro"] = $"Usuario Inativo";
                        else
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction(nameof(Index), "Home");
                        }                        
                    }                                    
                    else
                    {
                        TempData["MensagemErro"] = $"Login e/ou senha invalida.Por favor tente novamente!";
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task<IActionResult> Logout()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction(nameof(Index));
        }
    }
}
