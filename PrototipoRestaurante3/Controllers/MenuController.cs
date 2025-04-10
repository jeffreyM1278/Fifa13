using Microsoft.AspNetCore.Mvc;
using PrototipoRestaurante3.Models;
using Microsoft.EntityFrameworkCore;
namespace PrototipoRestaurante3.Controllers
{
    public class MenuController : Controller
    {
        private readonly restauranteDbContext _context;

        public MenuController(restauranteDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var promociones = await _context.Promocion.ToListAsync();
            var platos = await _context.Plato.Include(p => p.Promocion).ToListAsync();
            var combos = await _context.Combo.Include(c => c.Promocion).ToListAsync();

            var viewModel = new MenuViewModel
            {
                Promociones = promociones,
                Platos = platos,
                Combos = combos
            };

            return View(viewModel);
        }
    }
}
