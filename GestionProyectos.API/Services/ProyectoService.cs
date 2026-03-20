using GestionProyectos.API.Data;
using GestionProyectos.API.DTOs;
using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly ApplicationDbContext _context;
        public ProyectoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProyectoDTO>> GetProyectosAsync(string? buscar, int pagina, int cantidad)
        {
            var consulta = _context.Proyectos.AsQueryable();

            // 1. Aplicamos el filtro de busqueda
            if (!string.IsNullOrEmpty(buscar))
            {
                consulta = consulta.Where(p =>
                    p.Nombre.Contains(buscar) ||
                    p.Colaboradores.Any(c => c.Nombre.Contains(buscar))
                );
            }

            // 2. Aplicamos logica de paginacion
            // Skip: se salta los registros de paginas anteriores
            // Take: toma solo la cantidad solicitada
            return await consulta
                .Skip((pagina - 1) * cantidad) // Se calcularia el numero de pagina * cantidad por pagina
                .Take(cantidad) // Se indicaria la cantidad por pagina
                .Select(p => new ProyectoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CantidadColaboradores = p.Colaboradores.Count,
                    CantidadDocumentos = p.Documentos.Count
                })
                .ToListAsync();
        }

        public async Task<ProyectoDetalleDTO?> GetProyectoByIdAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.Colaboradores)
                .Include(p => p.Documentos)
                .Where(p => p.Id == id)
                .Select(p => new ProyectoDetalleDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Colaboradores = p.Colaboradores.Select(c => new ColaboradorResumenDTO
                    {
                        Nombre = c.Nombre,
                        Rol = c.Rol
                    }).ToList(),
                    Documentos = p.Documentos.Select(d => new DocumentoResumenDTO
                    {
                        Nombre = d.Nombre,
                        Enlace = d.Enlace
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Proyecto> CreateProyectoAsync(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
            return proyecto;
        }

        public async Task<bool> UpdateProyectoAsync(int id, Proyecto proyecto)
        {
            var proyectoExistente = await _context.Proyectos.FindAsync(id);
            if (proyectoExistente == null) return false;

            // Solo mapeamos lo que permitimos editar
            proyectoExistente.Nombre = proyecto.Nombre;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProyectoAsync(int id)
        {
            var proyecto = await _context.Proyectos
                .Include(p => p.Colaboradores)
                .Include(p => p.Documentos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null) return false;

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}