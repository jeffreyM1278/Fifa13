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
    public class DetalleCarritoController : Controller
    {
        private readonly restauranteDbContext _context;

        public DetalleCarritoController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: DetalleCarrito
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.DetalleCarrito.Include(d => d.Carrito).Include(d => d.Combo).Include(d => d.Plato);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: DetalleCarrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCarrito = await _context.DetalleCarrito
                .Include(d => d.Carrito)
                .Include(d => d.Combo)
                .Include(d => d.Plato)
                .FirstOrDefaultAsync(m => m.DetalleCarritoID == id);
            if (detalleCarrito == null)
            {
                return NotFound();
            }

            return View(detalleCarrito);
        }

        // GET: DetalleCarrito/Create
        public IActionResult Create()
        {
            ViewData["CarritoID"] = new SelectList(_context.CarritoCompra, "CarritoID", "CarritoID");
            ViewData["ComboID"] = new SelectList(_context.Combo, "ComboID", "ComboID");
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID");
            return View();
        }

        // POST: DetalleCarrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleCarritoID,CarritoID,PlatoID,ComboID,TipoItem,Cantidad,PrecioUnitario")] DetalleCarrito detalleCarrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleCarrito);
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
            ViewData["CarritoID"] = new SelectList(_context.CarritoCompra, "CarritoID", "CarritoID", detalleCarrito.CarritoID);
            ViewData["ComboID"] = new SelectList(_context.Combo, "ComboID", "ComboID", detalleCarrito.ComboID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", detalleCarrito.PlatoID);
            return View(detalleCarrito);
        }

        // GET: DetalleCarrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCarrito = await _context.DetalleCarrito.FindAsync(id);
            if (detalleCarrito == null)
            {
                return NotFound();
            }
            ViewData["CarritoID"] = new SelectList(_context.CarritoCompra, "CarritoID", "CarritoID", detalleCarrito.CarritoID);
            ViewData["ComboID"] = new SelectList(_context.Combo, "ComboID", "ComboID", detalleCarrito.ComboID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", detalleCarrito.PlatoID);
            return View(detalleCarrito);
        }

        // POST: DetalleCarrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleCarritoID,CarritoID,PlatoID,ComboID,TipoItem,Cantidad,PrecioUnitario")] DetalleCarrito detalleCarrito)
        {
            if (id != detalleCarrito.DetalleCarritoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleCarrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCarritoExists(detalleCarrito.DetalleCarritoID))
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
            ViewData["CarritoID"] = new SelectList(_context.CarritoCompra, "CarritoID", "CarritoID", detalleCarrito.CarritoID);
            ViewData["ComboID"] = new SelectList(_context.Combo, "ComboID", "ComboID", detalleCarrito.ComboID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", detalleCarrito.PlatoID);
            return View(detalleCarrito);
        }

        // GET: DetalleCarrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCarrito = await _context.DetalleCarrito
                .Include(d => d.Carrito)
                .Include(d => d.Combo)
                .Include(d => d.Plato)
                .FirstOrDefaultAsync(m => m.DetalleCarritoID == id);
            if (detalleCarrito == null)
            {
                return NotFound();
            }

            return View(detalleCarrito);
        }

        // POST: DetalleCarrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleCarrito = await _context.DetalleCarrito.FindAsync(id);
            if (detalleCarrito != null)
            {
                _context.DetalleCarrito.Remove(detalleCarrito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCarritoExists(int id)
        {
            return _context.DetalleCarrito.Any(e => e.DetalleCarritoID == id);
        }
    }
}
