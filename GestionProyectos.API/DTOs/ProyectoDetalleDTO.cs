namespace GestionProyectos.API.DTOs
{
    public class ProyectoDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<ColaboradorResumenDTO> Colaboradores { get; set; } = new();
        public List<DocumentoResumenDTO> Documentos { get; set; } = new();
    }

    public class ColaboradorResumenDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    public class DocumentoResumenDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Enlace { get; set; } = string.Empty;
    }
}
