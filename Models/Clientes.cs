﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public DateOnly FechaIngreso { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Direccion {  get; set; }

        [Required]
        [StringLength( 9, ErrorMessage = "El RNC debe tener entre 9 y 11 caracteres.")]
        public string Rnc {  get; set; }

        [Range(1, 100000, ErrorMessage = "El límite de crédito debe estar entre 0 y 100,000.")]
        public double LimiteCredito { get; set; }

        [ForeignKey("TecnicoId")]
        public int TecnicoId { get; set; }

        public virtual Tecnicos Tecnico { get; set; }

    }
}
