using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Azure.Core.HttpHeader;
namespace PrototipoRestaurante3.Models
{
    public class Promocion
    {
        [Key]
        public int PromocionID { get; set; }
        public string Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(30);
        [ValidateNever]
        public ICollection<Plato> Platos { get; set; }
        [ValidateNever]
        public ICollection<Combo> Combos { get; set; }
    }
}
