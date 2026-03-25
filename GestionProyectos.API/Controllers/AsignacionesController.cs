using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionProyectos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly IAsignacionService _asignacionService;
        public AsignacionesController(IAsignacionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar([FromBody] AsignacionRequest request)
        {
            var exito = await _asignacionService.AsignarUsuarioAProyectoAsync(
                request.UsuarioId,
                request.ProyectoId,
                request.RolEnProyecto
                );

            if (!exito) return BadRequest("El usuario ya esta asignado a este proyecto o hubo un error.");

            return Ok("Usuario asignado al proyecto exitosamente.");
        }

        [HttpGet("proyecto/{id}")]
        public async Task<IActionResult> GetColaboradores(int id)
        {
            var colaboradores = await _asignacionService.GetColaboradoresPorProyectoAsync(id);
            return Ok(colaboradores);
        }

    }

    public class AsignacionRequest
    {
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public string RolEnProyecto { get; set; } = string.Empty;
    }
}
