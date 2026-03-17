using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEstacionamento.Main.Data;
using SistemaEstacionamento.Main.Models;

namespace SistemaEstacionamento.Main.Controllers
{
    public class EstacionamentoController : Controller
    {
        private readonly SistemaEstacionamentoContext _context;      

        public EstacionamentoController(SistemaEstacionamentoContext context)
        {
            _context = context;
        }

        // GET: Estacionamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estacionamento.ToListAsync());
        }

        // GET: Estacionamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacionamento = await _context.Estacionamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estacionamento == null)
            {
                return NotFound();
            }

            return View(estacionamento);
        }

        // GET: Estacionamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estacionamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrecoInicial,PrecoHora,QuantidadeHoras,ValorTotal,PlacaVeiculo,ModeloVeiculo,Pago,DataEntrada,DataSaida")] Estacionamento estacionamento)
        {
            if (ModelState.IsValid)
            {
                estacionamento.DataEntrada = DateTime.Now;                
                estacionamento.ValorTotal = estacionamento.PrecoInicial;

                _context.Add(estacionamento);
                await _context.SaveChangesAsync();                
                return RedirectToAction(nameof(Index));
            }
            return View(estacionamento);
        }

        // GET: Estacionamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacionamento = await _context.Estacionamento.FindAsync(id);
            if (estacionamento == null)
            {
                return NotFound();
            }
           
            return View(estacionamento);
        }

        // POST: Estacionamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrecoInicial,PrecoHora,QuantidadeHoras,ValorTotal,PlacaVeiculo,ModeloVeiculo,Pago,DataEntrada,DataSaida")] Estacionamento estacionamento)
        {
            if (id != estacionamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    estacionamento.DataSaida = DateTime.Now;

                   // Calcula a diferença entre saída e entrada
                   TimeSpan diferenca = estacionamento.DataSaida.Value - estacionamento.DataEntrada;

                    // Se quiser o total de horas (inclui frações decimais)
                    estacionamento.QuantidadeHoras = decimal.Parse(diferenca.TotalHours.ToString());

                    if (estacionamento.QuantidadeHoras.HasValue && estacionamento.QuantidadeHoras > 1 && estacionamento.PrecoHoraAdicional.HasValue)
                        estacionamento.ValorTotal = estacionamento.PrecoInicial + estacionamento.PrecoHoraAdicional.Value * estacionamento.QuantidadeHoras.Value;
                    else
                        estacionamento.ValorTotal = estacionamento.PrecoInicial;

                    _context.Update(estacionamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstacionamentoExists(estacionamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estacionamento);
        }

        // GET: Estacionamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacionamento = await _context.Estacionamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estacionamento == null)
            {
                return NotFound();
            }

            return View(estacionamento);
        }

        // POST: Estacionamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estacionamento = await _context.Estacionamento.FindAsync(id);
            if (estacionamento != null)
            {
                if (estacionamento.Pago)
                    _context.Estacionamento.Remove(estacionamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstacionamentoExists(int id)
        {
            return _context.Estacionamento.Any(e => e.Id == id);
        }
    }
}
