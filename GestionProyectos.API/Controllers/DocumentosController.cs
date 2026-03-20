using GestionProyectos.API.Models;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Mvc;

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
            if(!actualizado) return NotFound();

            return NoContent();
        }

        // POST: api/Documentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Documento>> PostDocumento(Documento documento)
        {
            var nuevoDocumento = await _documentoService.CreateDocumentoAsync(documento);
            return CreatedAtAction(nameof(GetDocumento), new { id = documento.Id }, documento);
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var eliminado = await _documentoService.DeleteDocumentoAsync(id);
            if(!eliminado) return NotFound();

            return NoContent();
        }
    }
}
