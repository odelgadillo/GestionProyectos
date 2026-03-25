using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.API.Models
{
    public class AsignacionProyecto
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }

        [Required]
        public string RolEnProyecto { get; set; } = string.Empty;

        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
    }
}
