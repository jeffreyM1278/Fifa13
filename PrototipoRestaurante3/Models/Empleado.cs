using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace PrototipoRestaurante3.Models
{
    public class Empleado
    {
        [Key]
        public int EmpleadoID { get; set; }
        [Column("NombreEmp")]
        public string NombreEmp { get; set; }
        public decimal Sueldo { get; set; }
        public bool Activo { get; set; }

        // FK
        public int RolID { get; set; }
        [ValidateNever]
        public Rol Rol { get; set; }
    }
}
