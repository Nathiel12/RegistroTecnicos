using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Sistemas
    {
        [Key]
        public int SistemaId { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        public string Descripcion { get; set; }
        [Range(1, 100, ErrorMessage = "El nivel de complejidad debe estar entre 1 y 100.")]
        public double Complejidad { get; set; }
    }
}
