using Microsoft.AspNetCore.Mvc;
using PrototipoRestaurante3.Models;

namespace PrototipoRestaurante3.Controllers
{
    public class AccountController : Controller
    {
        private readonly restauranteDbContext _context;

        public AccountController(restauranteDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string contrasena)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Email == email && c.Contrasena == contrasena);

            if (cliente != null)
            {
                // Puedes guardar la sesión aquí si usas sesiones.
                TempData["ClienteNombre"] = cliente.Nombre;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas.";
                return View();
            }
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.FechaRegistro = DateTime.Now;
                _context.Cliente.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(cliente);
        }
    }
}
