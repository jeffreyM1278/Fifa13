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
    public class ComboController : Controller
    {
        private readonly restauranteDbContext _context;

        public ComboController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Combo
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.Combo.Include(c => c.Promocion);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: Combo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combo = await _context.Combo
                .Include(c => c.Promocion)
                .FirstOrDefaultAsync(m => m.ComboID == id);
            if (combo == null)
            {
                return NotFound();
            }

            return View(combo);
        }

        // GET: Combo/Create
        public IActionResult Create()
        {
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID");
            return View();
        }

        // POST: Combo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComboID,Nombre,Precio,RutaImagen,PromocionID")] Combo combo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(combo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Error: " + error.ErrorMessage);

                }
            }
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", combo.PromocionID);
            return View(combo);
        }

        // GET: Combo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combo = await _context.Combo.FindAsync(id);
            if (combo == null)
            {
                return NotFound();
            }
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", combo.PromocionID);
            return View(combo);
        }

        // POST: Combo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComboID,Nombre,Precio,RutaImagen,PromocionID")] Combo combo)
        {
            if (id != combo.ComboID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComboExists(combo.ComboID))
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
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", combo.PromocionID);
            return View(combo);
        }

        // GET: Combo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combo = await _context.Combo
                .Include(c => c.Promocion)
                .FirstOrDefaultAsync(m => m.ComboID == id);
            if (combo == null)
            {
                return NotFound();
            }

            return View(combo);
        }

        // POST: Combo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var combo = await _context.Combo.FindAsync(id);
            if (combo != null)
            {
                _context.Combo.Remove(combo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComboExists(int id)
        {
            return _context.Combo.Any(e => e.ComboID == id);
        }
    }
}
