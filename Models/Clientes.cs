using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        [Required]
        public DateOnly FechaIngreso { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Direccion {  get; set; }

        [Required]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "El RNC debe tener entre 9 y 11 caracteres.")]
        public string Rnc {  get; set; }

        [Required]
        public double LimiteCredito { get; set; }
        public int TecnicoId { get; set; }
        
    }
}
