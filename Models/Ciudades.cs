using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Ciudades
    {
        [Key]
        public int CiudadId { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
    }
}
