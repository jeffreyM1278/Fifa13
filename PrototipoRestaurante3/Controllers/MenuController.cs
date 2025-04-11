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

        // Acción para agregar al carrito
        public IActionResult AgregarAlCarrito(int? platoId, int? comboId)
        {
            int clienteId = HttpContext.Session.GetInt32("ClienteID") ?? 0;

            if (clienteId == 0)
            {
                return RedirectToAction("Index", "Home"); // Redirigir a la página de login si el cliente no está logueado
            }

            // Verificar si ya existe un carrito activo
            var carrito = _context.CarritoCompra
                .FirstOrDefault(c => c.ClienteID == clienteId && c.Estado == "Activo");

            if (carrito == null)
            {
                // Si no existe un carrito activo, crearlo
                carrito = new CarritoCompra
                {
                    ClienteID = clienteId,
                    Estado = "Activo", // Estado "Activo" para carritos en uso
                    FechaCreacion = DateTime.Now // Asignar la fecha y hora actuales
                };
                _context.CarritoCompra.Add(carrito);
                _context.SaveChanges();  // Guardar el carrito recién creado
            }

            // Agregar el plato al carrito
            if (platoId.HasValue)
            {
                var plato = _context.Plato.Find(platoId.Value);
                if (plato != null)
                {
                    var detalle = new DetalleCarrito
                    {
                        CarritoID = carrito.CarritoID,
                        PlatoID = plato.PlatoID,
                        TipoItem = "Plato", // Especificamos que es un plato
                        Cantidad = 1, // La cantidad puede ser modificada si lo deseas
                        PrecioUnitario = plato.Precio, // Precio del plato
                        Subtotal = plato.Precio // Calculamos el subtotal
                    };
                    _context.DetalleCarrito.Add(detalle);
                }
            }

            // Agregar el combo al carrito
            if (comboId.HasValue)
            {
                var combo = _context.Combo.Find(comboId.Value);
                if (combo != null)
                {
                    var detalle = new DetalleCarrito
                    {
                        CarritoID = carrito.CarritoID,
                        ComboID = combo.ComboID,
                        TipoItem = "Combo", // Especificamos que es un combo
                        Cantidad = 1, // La cantidad puede ser modificada si lo deseas
                        PrecioUnitario = combo.Precio, // Precio del combo
                        Subtotal = combo.Precio // Calculamos el subtotal
                    };
                    _context.DetalleCarrito.Add(detalle);
                }
            }

            _context.SaveChanges(); // Guardamos los detalles del carrito

            return RedirectToAction("Index", "Carrito"); // Redirige al carrito
        }
    }

}
