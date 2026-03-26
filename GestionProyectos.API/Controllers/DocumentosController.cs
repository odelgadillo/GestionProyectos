using GestionProyectos.API.Models;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;

namespace GestionProyectos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;

        public DocumentosController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumentos()
        {
            var documentos = await _documentoService.GetDocumentosAsync();
            return Ok(documentos);
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumento(int id)
        {
            var documento = await _documentoService.GetDocumentoByIdAsync(id);
            if (documento == null) return NotFound();
            return Ok(documento);
        }

        // PUT: api/Documentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(int id, Documento documento)
        {
            if (id != documento.Id) return BadRequest();
            var actualizado = await _documentoService.UpdateDocumentoAsync(id, documento);
            if (!actualizado) return NotFound();

            return NoContent();
        }

        // POST: api/Documentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("subir/{proyectoId}")]
        [Authorize]
        public async Task<ActionResult> PostDocumento(int proyectoId, IFormFile archivo)
        {
            // 1. Extraer el UsuarioId del token JWT
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("Token inválido");

            int usuarioId = int.Parse(userIdClaim);

            // 2. Validacion del archivo
            if (archivo == null || archivo.Length == 0) return BadRequest("No se ha seleccionado ningún archivo.");

            try
            {
                // 3. LLamar al servicio que guarda en disco y base de datos
                var nuevoDoc = await _documentoService.CreateDocumentoAsync(proyectoId, usuarioId, archivo);

                return CreatedAtAction(nameof(GetDocumento), new { id = nuevoDoc.Id }, nuevoDoc);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Si no es miembro del proyecto, el servicio lanza una excepción.
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir el archivo: {ex.Message}");
            }
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var eliminado = await _documentoService.DeleteDocumentoAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
