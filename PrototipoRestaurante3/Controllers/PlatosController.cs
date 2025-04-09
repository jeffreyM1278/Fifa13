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
    public class PlatosController : Controller
    {
        private readonly restauranteDbContext _context;

        public PlatosController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Platos
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.Plato.Include(p => p.Promocion);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: Platos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .Include(p => p.Promocion)
                .FirstOrDefaultAsync(m => m.PlatoID == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Platos/Create
        public IActionResult Create()
        {
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID");
            return View();
        }

        // POST: Platos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatoID,Nombre,Precio,RutaImagen,PromocionID")] Plato plato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plato);
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
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", plato.PromocionID);
            return View(plato);
        }

        // GET: Platos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", plato.PromocionID);
            return View(plato);
        }

        // POST: Platos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlatoID,Nombre,Precio,RutaImagen,PromocionID")] Plato plato)
        {
            if (id != plato.PlatoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(plato.PlatoID))
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
            ViewData["PromocionID"] = new SelectList(_context.Promocion, "PromocionID", "PromocionID", plato.PromocionID);
            return View(plato);
        }

        // GET: Platos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plato = await _context.Plato
                .Include(p => p.Promocion)
                .FirstOrDefaultAsync(m => m.PlatoID == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Plato.FindAsync(id);
            if (plato != null)
            {
                _context.Plato.Remove(plato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoExists(int id)
        {
            return _context.Plato.Any(e => e.PlatoID == id);
        }
    }
}
