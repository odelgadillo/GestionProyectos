using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Usuario";

        [Required]
        [StringLength(20)]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
