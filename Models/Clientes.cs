using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        [Required]
        public DateOnly FechaIngreso { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Direccion {  get; set; }

        [Required]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "El RNC debe tener entre 9 y 11 caracteres.")]
        public string Rnc {  get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public double LimiteCredito { get; set; }

        [ForeignKey("TecnicoId")]
        public int TecnicoId { get; set; }

        public virtual Tecnicos Tecnico { get; set; }

    }
}
