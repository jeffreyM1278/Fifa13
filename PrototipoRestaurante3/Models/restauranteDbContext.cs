using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
namespace PrototipoRestaurante3.Models
{
    public class restauranteDbContext : DbContext
    {
        public restauranteDbContext(DbContextOptions<restauranteDbContext> options) : base(options)
        {
        }
        public DbSet<Rol> Rol { get; set; } //1
        public DbSet<Empleado> Empleado { get; set; } //2
        public DbSet<Promocion> Promocion { get; set; } //3
        public DbSet<Plato> Plato { get; set; } //4
        public DbSet<Combo> Combo { get; set; } //5
        public DbSet<TipoPago> TipoPago { get; set; } //6 Hasta aqui estan terminadas bien(modelado bien hecho)
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<PedidoOnline> PedidoOnline { get; set; }
        public DbSet<DetallePedidoOnline> DetallePedidoOnline { get; set; }
        public DbSet<PlatoComprado> PlatoComprado { get; set; }
        public DbSet<CarritoCompra> CarritoCompra { get; set; }
        public DbSet<DetalleCarrito> DetalleCarrito { get; set; }
    }
}
