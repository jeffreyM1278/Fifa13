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
    public class PedidoOnlinController : Controller
    {
        private readonly restauranteDbContext _context;

        public PedidoOnlinController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: PedidoOnlin
        public async Task<IActionResult> Index()
        {
            var restauranteDbContext = _context.PedidoOnline.Include(p => p.Cliente).Include(p => p.TipoPago);
            return View(await restauranteDbContext.ToListAsync());
        }

        // GET: PedidoOnlin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoOnline = await _context.PedidoOnline
                .Include(p => p.Cliente)
                .Include(p => p.TipoPago)
                .FirstOrDefaultAsync(m => m.PedidoID == id);
            if (pedidoOnline == null)
            {
                return NotFound();
            }

            return View(pedidoOnline);
        }

        // GET: PedidoOnlin/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID");
            ViewData["TipoPagoID"] = new SelectList(_context.TipoPago, "TipoPagoID", "TipoPagoID");
            return View();
        }

        // POST: PedidoOnlin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoID,ClienteID,TipoPagoID,FechaPedido,Total,Estado")] PedidoOnline pedidoOnline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoOnline);
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", pedidoOnline.ClienteID);
            ViewData["TipoPagoID"] = new SelectList(_context.TipoPago, "TipoPagoID", "TipoPagoID", pedidoOnline.TipoPagoID);
            return View(pedidoOnline);
        }

        // GET: PedidoOnlin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoOnline = await _context.PedidoOnline.FindAsync(id);
            if (pedidoOnline == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", pedidoOnline.ClienteID);
            ViewData["TipoPagoID"] = new SelectList(_context.TipoPago, "TipoPagoID", "TipoPagoID", pedidoOnline.TipoPagoID);
            return View(pedidoOnline);
        }

        // POST: PedidoOnlin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoID,ClienteID,TipoPagoID,FechaPedido,Total,Estado")] PedidoOnline pedidoOnline)
        {
            if (id != pedidoOnline.PedidoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoOnline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoOnlineExists(pedidoOnline.PedidoID))
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", pedidoOnline.ClienteID);
            ViewData["TipoPagoID"] = new SelectList(_context.TipoPago, "TipoPagoID", "TipoPagoID", pedidoOnline.TipoPagoID);
            return View(pedidoOnline);
        }

        // GET: PedidoOnlin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoOnline = await _context.PedidoOnline
                .Include(p => p.Cliente)
                .Include(p => p.TipoPago)
                .FirstOrDefaultAsync(m => m.PedidoID == id);
            if (pedidoOnline == null)
            {
                return NotFound();
            }

            return View(pedidoOnline);
        }

        // POST: PedidoOnlin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoOnline = await _context.PedidoOnline.FindAsync(id);
            if (pedidoOnline != null)
            {
                _context.PedidoOnline.Remove(pedidoOnline);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoOnlineExists(int id)
        {
            return _context.PedidoOnline.Any(e => e.PedidoID == id);
        }
    }
}
