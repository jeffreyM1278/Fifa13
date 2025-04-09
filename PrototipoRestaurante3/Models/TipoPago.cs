using System.ComponentModel.DataAnnotations;
namespace PrototipoRestaurante3.Models
{
    public class TipoPago
    {
        [Key]
        public int TipoPagoID { get; set; }
        public string Tipo { get; set; }
    }
}
