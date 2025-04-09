using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    public class Plato
    {
        [Key]
        public int PlatoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string RutaImagen { get; set; }

        // FK
        public int? PromocionID { get; set; }
        [ValidateNever]
        public Promocion Promocion { get; set; }
    }
}