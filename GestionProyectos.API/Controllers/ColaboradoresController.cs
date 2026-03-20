using GestionProyectos.API.Models;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionProyectos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradoresController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        // GET: api/Colaboradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradores()
        {
            var colaboradores = await _colaboradorService.GetColaboradoresAsync();
            return Ok(colaboradores);
        }

        // GET: api/Colaboradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
            var colaborador = await _colaboradorService.GetColaboradorByIdAsync(id);
            if (colaborador == null) return NotFound();
            return Ok(colaborador);
        }

        // PUT: api/Colaboradores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id) return BadRequest();

            var actualizado = await _colaboradorService.UpdateColaboradorAsync(id, colaborador);
            if (!actualizado) return NotFound();

            return NoContent();
        }

        // POST: api/Colaboradores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
            var nuevoColaborador = await _colaboradorService.CreateColaboradorAsync(colaborador);
            return CreatedAtAction(nameof(GetColaborador), new { id = colaborador.Id }, colaborador);
        }

        // DELETE: api/Colaboradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            var eliminado = await _colaboradorService.DeleteColaboradorAsync(id);
            if(!eliminado) return NotFound();
            
            return NoContent();
        }
    }
}
