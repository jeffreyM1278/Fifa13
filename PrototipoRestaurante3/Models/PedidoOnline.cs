using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    [Table("PedidosOnline")]
    public class PedidoOnline
    {
        [Key]
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public int TipoPagoID { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string Estado { get; set; }
        [ValidateNever]
        public Cliente Cliente { get; set; }
        [ValidateNever]
        public TipoPago TipoPago { get; set; }
        [ValidateNever]
        public ICollection<DetallePedidoOnline> Detalles { get; set; }
    }
}
