using GestionProyectos.API.DTOs;
using GestionProyectos.API.Models;

namespace GestionProyectos.API.Services
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoDTO>> GetProyectosAsync(string? buscar, int pagina, int cantidad);
        Task<ProyectoDetalleDTO?> GetProyectoByIdAsync(int id);
        Task<Proyecto> CreateProyectoAsync(Proyecto proyecto);
        Task<bool> UpdateProyectoAsync(int id, Proyecto proyecto);
        Task<bool> DeleteProyectoAsync(int id);
        Task<IEnumerable<Proyecto>> GetProyectosParaUsuarioAsync(int usuarioId, string rolSistema);
    }
}