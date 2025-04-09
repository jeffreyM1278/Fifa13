using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrototipoRestaurante3.Models
{
    public class CarritoCompra
    {
        [Key]
        public int CarritoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } // Activo, Pendiente, Finalizado
        [ValidateNever]
        public Cliente Cliente { get; set; }
        [ValidateNever]
        public ICollection<DetalleCarrito> Detalles { get; set; }
    }
}
