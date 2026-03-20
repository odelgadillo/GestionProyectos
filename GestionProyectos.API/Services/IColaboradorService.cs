using GestionProyectos.API.Models;

namespace GestionProyectos.API.Services
{
    public interface IColaboradorService
    {
        Task<IEnumerable<Colaborador>> GetColaboradoresAsync();
        Task<Colaborador?> GetColaboradorByIdAsync(int id);
        Task<Colaborador> CreateColaboradorAsync(Colaborador colaborador);
        Task<bool> UpdateColaboradorAsync(int id, Colaborador colaborador);
        Task<bool> DeleteColaboradorAsync(int id);
    }
}
