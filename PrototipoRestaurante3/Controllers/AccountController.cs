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

                
                HttpContext.Session.SetInt32("ClienteID", cliente.ClienteID); // Aquí guardas el ID del cliente
                HttpContext.Session.SetString("ClienteNombre", cliente.Nombre);
                // Puedes guardar la sesión aquí si usas sesiones.
                TempData["ClienteNombre"] = cliente.Nombre;
                return RedirectToAction("Index", "Menu");
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

        // Método Logout para cerrar sesión
        public IActionResult Logout()
        {
            Console.WriteLine("Logout iniciado");

            // Limpiar las variables de sesión
            HttpContext.Session.Clear();
            TempData["Message"] = "Sesión cerrada con éxito";

            Console.WriteLine("Redirigiendo a Login");

            // Redirigir a la acción Login
            return RedirectToAction("Index", "Home");
        }


    }
}
