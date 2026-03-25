namespace GestionProyectos.API.Services
{
    public interface IAsignacionService
    {
        Task<bool> AsignarUsuarioAProyectoAsync(int usuarioId, int proyectoId, string rolEnProyecto);
        Task<IEnumerable<object>> GetColaboradoresPorProyectoAsync(int proyectoId);
    }
}
