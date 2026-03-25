using GestionProyectos.API.DTOs;
using GestionProyectos.API.Models;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Pkcs;

namespace GestionProyectos.API.Controllers
{
    [Authorize]
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
        [HttpGet("Todos")]
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRoleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
            {
                return Unauthorized("No se pudo identirficar al usuario desde el token.");
            }

            int usuarioId = int.Parse(userIdClaim);
            string rolSistema = userRoleClaim;

            var proyectos = await _proyectoService.GetProyectosParaUsuarioAsync(usuarioId, rolSistema);
            return Ok(proyectos);
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
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
            var nuevoProyecto = await _proyectoService.CreateProyectoAsync(proyecto);
            return CreatedAtAction(nameof(GetProyecto), new { id = nuevoProyecto.Id }, nuevoProyecto);
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var eliminado = await _proyectoService.DeleteProyectoAsync(id);
            if(!eliminado) return NotFound();

            return NoContent();
        }
    }
}
