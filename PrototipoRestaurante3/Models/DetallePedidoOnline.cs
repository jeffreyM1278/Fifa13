using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    public class DetallePedidoOnline
    {
        [Key]
        public int DetalleID { get; set; }
        public int PedidoID { get; set; }
        public int? PlatoID { get; set; }
        public int? ComboID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        [ValidateNever]
        public PedidoOnline Pedido { get; set; }
        [ValidateNever]
        public Plato Plato { get; set; }
        [ValidateNever]
        public Combo Combo { get; set; }

        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
