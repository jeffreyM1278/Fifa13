using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrototipoRestaurante3.Models
{
    public class Combo
    {
        [Key]
        public int ComboID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string RutaImagen { get; set; }

        public int? PromocionID { get; set; }
        [ValidateNever]
        public Promocion Promocion { get; set; }
    }
}
