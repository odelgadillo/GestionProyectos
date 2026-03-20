using GestionProyectos.API.Attributes;
using GestionProyectos.API.Data;
using GestionProyectos.API.DTOs;
using GestionProyectos.API.Models;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;
        public ProyectosController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectoDTO>>> GetProyectos(string? buscar, int pagina = 1, int cantidad = 10)
        {
            var proyectos = await _proyectoService.GetProyectosAsync(buscar, pagina, cantidad);
            return Ok(proyectos);
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDetalleDTO>> GetProyecto(int id)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null) return NotFound();
            return Ok(proyecto);
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id) return BadRequest();

            var acualizado = await _proyectoService.UpdateProyectoAsync(id, proyecto);
            if(!acualizado) return NotFound();

            return NoContent();
        }

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
            var nuevoProyecto = await _proyectoService.CreateProyectoAsync(proyecto);
            return CreatedAtAction(nameof(GetProyecto), new { id = nuevoProyecto.Id }, nuevoProyecto);
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var eliminado = await _proyectoService.DeleteProyectoAsync(id);
            if(!eliminado) return NotFound();

            return NoContent();
        }
    }
}
