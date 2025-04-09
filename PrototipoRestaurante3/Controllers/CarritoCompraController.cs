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
    public class CarritoCompraController : Controller
    {
        private readonly restauranteDbContext _context;

        public CarritoCompraController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: CarritoCompra
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.CarritoCompra.Include(c => c.Cliente);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: CarritoCompra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarritoID == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // GET: CarritoCompra/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID");
            return View();
        }

        // POST: CarritoCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoID,ClienteID,FechaCreacion,Estado")] CarritoCompra carritoCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoCompra);
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", carritoCompra.ClienteID);
            return View(carritoCompra);
        }

        // GET: CarritoCompra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra.FindAsync(id);
            if (carritoCompra == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", carritoCompra.ClienteID);
            return View(carritoCompra);
        }

        // POST: CarritoCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarritoID,ClienteID,FechaCreacion,Estado")] CarritoCompra carritoCompra)
        {
            if (id != carritoCompra.CarritoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoCompraExists(carritoCompra.CarritoID))
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", carritoCompra.ClienteID);
            return View(carritoCompra);
        }

        // GET: CarritoCompra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarritoID == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // POST: CarritoCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoCompra = await _context.CarritoCompra.FindAsync(id);
            if (carritoCompra != null)
            {
                _context.CarritoCompra.Remove(carritoCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoCompraExists(int id)
        {
            return _context.CarritoCompra.Any(e => e.CarritoID == id);
        }
    }
}
