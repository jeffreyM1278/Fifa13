using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    [Table("PlatosComprados")]
    public class PlatoComprado
    {
        [Key]
        public int CompraID { get; set; }
        public int ClienteID { get; set; }
        public int PlatoID { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
        [ValidateNever]
        public Cliente Cliente { get; set; }
        [ValidateNever]
        public Plato Plato { get; set; }
    }
}
