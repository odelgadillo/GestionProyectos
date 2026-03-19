using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.API.Models
{
    public class Documento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Enlace { get; set; } = string.Empty;

        public string Tipo { get; set; } = "Enlace"; // Por defecto, se asume que es un enlace

        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}