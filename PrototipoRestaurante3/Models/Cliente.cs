using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string Contrasena { get; set; }
        [ValidateNever]
        public ICollection<PedidoOnline> PedidosOnline { get; set; }
        [ValidateNever]
        public ICollection<PlatoComprado> PlatosComprados { get; set; }
        [ValidateNever]
        public ICollection<CarritoCompra> Carritos { get; set; }
    }
}
