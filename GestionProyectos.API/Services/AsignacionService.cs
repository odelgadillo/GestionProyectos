using GestionProyectos.API.Data;
using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Services
{
    public class AsignacionService : IAsignacionService
    {
        private readonly ApplicationDbContext _context;
        public AsignacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AsignarUsuarioAProyectoAsync(int usuarioId, int proyectoId, string rolEnProyecto)
        {
            var existe = await _context.AsignacionesProyectos
                .AnyAsync(a => a.UsuarioId == usuarioId && a.ProyectoId == proyectoId);

            if (existe) return false;

            var nuevaASignacion = new AsignacionProyecto
            {
                UsuarioId = usuarioId,
                ProyectoId = proyectoId,
                RolEnProyecto = rolEnProyecto,
                FechaAsignacion = DateTime.UtcNow
            };

            _context.AsignacionesProyectos.Add(nuevaASignacion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<object>> GetColaboradoresPorProyectoAsync(int proyectoId)
        {
            return await _context.AsignacionesProyectos
                .Where(a => a.ProyectoId == proyectoId)
                .Select(a => new
                {
                    UsuarioId = a.UsuarioId,
                    NombreCompleto = a.Usuario!.NombreCompleto,
                    RolEnProyecto = a.RolEnProyecto,
                    Email = a.Usuario.Email,
                })
                .ToListAsync();
        }
    }
}
