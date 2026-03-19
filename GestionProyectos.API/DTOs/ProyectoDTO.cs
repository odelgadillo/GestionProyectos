namespace GestionProyectos.API.DTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CantidadColaboradores { get; set; }
        public int CantidadDocumentos { get; set; }
    }
}
