namespace GestionProyectos.API.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
