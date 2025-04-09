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
    public class PlatoCompradoController : Controller
    {
        private readonly restauranteDbContext _context;

        public PlatoCompradoController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: PlatoComprado
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.PlatoComprado.Include(p => p.Cliente).Include(p => p.Plato);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: PlatoComprado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoComprado = await _context.PlatoComprado
                .Include(p => p.Cliente)
                .Include(p => p.Plato)
                .FirstOrDefaultAsync(m => m.CompraID == id);
            if (platoComprado == null)
            {
                return NotFound();
            }

            return View(platoComprado);
        }

        // GET: PlatoComprado/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID");
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID");
            return View();
        }

        // POST: PlatoComprado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraID,ClienteID,PlatoID,Cantidad,FechaCompra")] PlatoComprado platoComprado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platoComprado);
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", platoComprado.ClienteID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", platoComprado.PlatoID);
            return View(platoComprado);
        }

        // GET: PlatoComprado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoComprado = await _context.PlatoComprado.FindAsync(id);
            if (platoComprado == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", platoComprado.ClienteID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", platoComprado.PlatoID);
            return View(platoComprado);
        }

        // POST: PlatoComprado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraID,ClienteID,PlatoID,Cantidad,FechaCompra")] PlatoComprado platoComprado)
        {
            if (id != platoComprado.CompraID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platoComprado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoCompradoExists(platoComprado.CompraID))
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", platoComprado.ClienteID);
            ViewData["PlatoID"] = new SelectList(_context.Plato, "PlatoID", "PlatoID", platoComprado.PlatoID);
            return View(platoComprado);
        }

        // GET: PlatoComprado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoComprado = await _context.PlatoComprado
                .Include(p => p.Cliente)
                .Include(p => p.Plato)
                .FirstOrDefaultAsync(m => m.CompraID == id);
            if (platoComprado == null)
            {
                return NotFound();
            }

            return View(platoComprado);
        }

        // POST: PlatoComprado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platoComprado = await _context.PlatoComprado.FindAsync(id);
            if (platoComprado != null)
            {
                _context.PlatoComprado.Remove(platoComprado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoCompradoExists(int id)
        {
            return _context.PlatoComprado.Any(e => e.CompraID == id);
        }
    }
}
