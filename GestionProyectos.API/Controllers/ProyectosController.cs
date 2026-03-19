using GestionProyectos.API.Data;
using GestionProyectos.API.DTOs;
using GestionProyectos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectos(string? buscar)
        {
            // 1. Creamos la consulta base (Queryable)
            var consulta = _context.Proyectos.AsQueryable();

            // 2. Si el usuario envio algo para buscar, filtramos
            if (!string.IsNullOrEmpty(buscar))
            {
                consulta = consulta.Where(p => 
                    p.Nombre.Contains(buscar) ||
                    p.Colaboradores.Any(c => c.Nombre.Contains(buscar))
                );
            }

            // 3. Transformamos al DTO y ejecutamos la consulta

            return await consulta
                .Select(p => new ProyectoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CantidadColaboradores = p.Colaboradores.Count,
                    CantidadDocumentos = p.Documentos.Count
                })
                .ToListAsync();
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDetalleDTO>> GetProyecto(int id)
        {
            var proyecto = await _context.Proyectos
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

            if (proyecto == null) return NotFound();

            return proyecto;
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProyecto", new { id = proyecto.Id }, proyecto);
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
