using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.API.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int ProyectoId { get; set; }

        public Proyecto? Proyecto { get; set; }
    }
}
