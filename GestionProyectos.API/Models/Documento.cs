using System.ComponentModel.DataAnnotations;

namespace GestionProyectos.API.Models
{
    public class Documento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del documento es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage ="El enlace o ruta es obligatorio.")]
        [Url(ErrorMessage = "Por favor, ingresa una URL válida (http:// o https://.")]
        public string Enlace { get; set; } = string.Empty;

        [RegularExpression("^(PDF|Excel|Word|URL|Carpeta)$", ErrorMessage = "El tipo debe ser PDF, Excel, Word, URL o Carpeta.")]
        public string Tipo { get; set; } = "URL";

        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}