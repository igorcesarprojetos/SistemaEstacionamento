using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SistemaEstacionamento.Main.Data;
using SistemaEstacionamento.Main.Models;
using SistemaEstacionamento.Main.Utilitarios.Helper;
using SistemaEstacionamento.Main.Utilitarios.Helper.Interfaces;
using SistemaEstacionamento.Main.Utilitarios.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEstacionamento.Main.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SistemaEstacionamentoContext _context;
        private readonly IEmailSenders _emailSenders;
        private readonly ISessao _sessao;

        public UsuariosController(SistemaEstacionamentoContext context, IEmailSenders emailSenders, ISessao sessao)
        {
            _context = context;
            _emailSenders = emailSenders;
            _sessao = sessao;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null)
                return View(await _context.Usuario.ToListAsync());
            else
                return RedirectToAction(nameof(Index), "Autenticacao");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            TempData["Sucesso"] = null;
            TempData["EmailEnviado"] = null;
            TempData["Erro"] = null;
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Login,Senha,Email,IndAtivo")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senhaDescrypt = usuario.Senha;

                    var password = HelperSHA256.Encrypt(usuario.Senha);
                    usuario.Senha = password;

                    _context.Add(usuario);
                    if (await _context.SaveChangesAsync() > 0)
                        TempData["Sucesso"] = "Usuario salvo com sucesso!";                   

                    var emailEnviado = await _emailSenders
                        .SendEmailAsync(
                           usuario.Email,
                          "Sistema Estacionamento: Criação de Usuario e senha",
                          "Este é seu acesso:",
                          $"<span style='font-size:16px'><b>usuário:</b>{usuario.Login}</span></br> <span style='font-size:16px'><b>senha:</b>{senhaDescrypt}</span></br>"
                        );

                    if (emailEnviado)
                        TempData["EmailEnviado"] = $"E-mail de usuario e senha enviado com sucesso para o email: {usuario.Email}!";


                    if (usuario != null && usuario.Id > 0)
                    {
                        if (!usuario.IndAtivo)
                            TempData["Erro"] = $"Usuario Inativo";
                        else
                        {
                            if (_sessao.BuscarSessaoUsuario() == null)
                            {
                                _sessao.CriarSessaoUsuario(usuario);
                                return RedirectToAction(nameof(Index), "Home");
                            }                             
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                throw new Exception(TempData["Erro"]?.ToString());
            }

        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Login,Senha,Email,IndAtivo")] Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    if (usuario.Senha != HelperSHA256.Encrypt(usuario.Senha))
                    {
                        var password = HelperSHA256.Encrypt(usuario.Senha);
                        usuario.Senha = password;
                    }

                    _context.Update(usuario);

                    if (await _context.SaveChangesAsync() > 0)
                        TempData["Sucesso"] = "Usuario editado com sucesso!";

                    return RedirectToAction(nameof(Index));                    
                }
                return View(usuario);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                TempData["Erro"] = ex.Message;
                if (!UsuarioExists(usuario.Id))
                {                    
                    return NotFound();
                }
                else
                {
                    throw new Exception(TempData["Erro"]?.ToString());                    
                }                
            }
            
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                }

                if(await _context.SaveChangesAsync()>0)
                    TempData["Sucesso"] = "Usuario deletado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;

                throw new Exception(TempData["Erro"]?.ToString());
            }
            
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
