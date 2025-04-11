using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrototipoRestaurante3.Models
{
    public class DetalleCarrito
    {
        [Key]
        public int DetalleCarritoID { get; set; }
        public int CarritoID { get; set; }
        public int? PlatoID { get; set; }
        public int? ComboID { get; set; }
        public string TipoItem { get; set; } // 'Plato' o 'Combo'
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        [ValidateNever]
        public CarritoCompra Carrito { get; set; }
        [ValidateNever]
        public Plato Plato { get; set; }
        [ValidateNever]
        public Combo Combo { get; set; }

        [NotMapped]
        public decimal Subtotal { get; set; }
    }
}
