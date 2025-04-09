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
    public class PromocionController : Controller
    {
        private readonly restauranteDbContext _context;

        public PromocionController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: Promocion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promocion.ToListAsync());
        }

        // GET: Promocion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocion
                .FirstOrDefaultAsync(m => m.PromocionID == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // GET: Promocion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promocion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromocionID,Descripcion,Descuento,FechaInicio,FechaFin")] Promocion promocion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocion);
        }

        // GET: Promocion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocion.FindAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }
            return View(promocion);
        }

        // POST: Promocion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromocionID,Descripcion,Descuento,FechaInicio,FechaFin")] Promocion promocion)
        {
            if (id != promocion.PromocionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionExists(promocion.PromocionID))
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
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Error: " + error.ErrorMessage);

                }
            }
            return View(promocion);
        }

        // GET: Promocion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocion
                .FirstOrDefaultAsync(m => m.PromocionID == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // POST: Promocion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promocion = await _context.Promocion.FindAsync(id);
            if (promocion != null)
            {
                _context.Promocion.Remove(promocion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionExists(int id)
        {
            return _context.Promocion.Any(e => e.PromocionID == id);
        }
    }
}
