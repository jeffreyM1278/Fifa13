using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrototipoRestaurante3.Models;

namespace PrototipoRestaurante3.Controllers
{


    public class CarritoController : Controller
    {
        private readonly restauranteDbContext _context;

        public CarritoController(restauranteDbContext context)
        {
            _context = context;
        }

        // Mostrar los productos en el carrito
        public IActionResult Index()
        {
            int clienteId = HttpContext.Session.GetInt32("ClienteID") ?? 0;

            var carrito = _context.CarritoCompra
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Plato)
                .Where(c => c.ClienteID == clienteId && c.Estado == "Activo")
                .FirstOrDefault();

            if (carrito == null)
            {
                return RedirectToAction("Index", "Menu"); // Si no hay carrito activo, redirige al menú
            }

            return View(carrito);
        }

        // Mostrar la vista de personalización del carrito
        public IActionResult PersonalizarCompra(int carritoId)
        {
            var carrito = _context.CarritoCompra
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Plato)
                .FirstOrDefault(c => c.CarritoID == carritoId);

            if (carrito == null)
            {
                return RedirectToAction("Index", "Menu");
            }

            return View(carrito);
        }

        // Guardar los cambios de personalización del carrito
        [HttpPost]
        public IActionResult GuardarCambios(int carritoId, List<DetalleCarrito> detallesActualizados)
        {
            var carrito = _context.CarritoCompra
                .Include(c => c.Detalles)
                .FirstOrDefault(c => c.CarritoID == carritoId);

            if (carrito != null)
            {
                // Asegúrate de que la cantidad se actualiza correctamente
                foreach (var detalle in detallesActualizados)
                {
                    var detalleExistente = carrito.Detalles
                        .FirstOrDefault(d => d.DetalleCarritoID == detalle.DetalleCarritoID);
                    if (detalleExistente != null)
                    {
                        detalleExistente.Cantidad = detalle.Cantidad;
                        _context.Update(detalleExistente);
                    }
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Carrito");
        }

        // Mostrar el resumen de la compra antes de finalizar
        public IActionResult FinalizarCompra(int carritoId)
        {
            var carrito = _context.CarritoCompra
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Plato)
                .FirstOrDefault(c => c.CarritoID == carritoId);

            if (carrito == null)
            {
                return RedirectToAction("Index", "Menu");
            }

            decimal total = carrito.Detalles.Sum(d => d.Subtotal);
            ViewBag.Total = total;

            return View();
        }
    }


}
