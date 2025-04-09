using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrototipoRestaurante3.Models
{
    public class Rol
    {
        [Key]
        public int RolID { get; set; }
        [Column("NombreRol")]
        public string Nombre { get; set; }

        // Relaciones
        [ValidateNever]
        public ICollection<Empleado> Empleados { get; set; }
    }
}
