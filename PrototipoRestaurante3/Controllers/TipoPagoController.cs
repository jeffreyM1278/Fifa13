using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrototipoRestaurante3.Models;

namespace PrototipoRestaurante3.Controllers
{
    public class TipoPagoController : Controller
    {
        private readonly restauranteDbContext _context;

        public TipoPagoController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: TipoPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoPago.ToListAsync());
        }

        // GET: TipoPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPago = await _context.TipoPago
                .FirstOrDefaultAsync(m => m.TipoPagoID == id);
            if (tipoPago == null)
            {
                return NotFound();
            }

            return View(tipoPago);
        }

        // GET: TipoPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPagoID,Tipo")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPago);
        }

        // GET: TipoPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPago = await _context.TipoPago.FindAsync(id);
            if (tipoPago == null)
            {
                return NotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoPagoID,Tipo")] TipoPago tipoPago)
        {
            if (id != tipoPago.TipoPagoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPagoExists(tipoPago.TipoPagoID))
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
            return View(tipoPago);
        }

        // GET: TipoPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPago = await _context.TipoPago
                .FirstOrDefaultAsync(m => m.TipoPagoID == id);
            if (tipoPago == null)
            {
                return NotFound();
            }

            return View(tipoPago);
        }

        // POST: TipoPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoPago = await _context.TipoPago.FindAsync(id);
            if (tipoPago != null)
            {
                _context.TipoPago.Remove(tipoPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPagoExists(int id)
        {
            return _context.TipoPago.Any(e => e.TipoPagoID == id);
        }
    }
}
