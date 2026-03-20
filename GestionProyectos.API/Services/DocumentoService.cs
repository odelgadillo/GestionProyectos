using GestionProyectos.API.Data;
using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Services
{
    public class DocumentoService: IDocumentoService
    {
        private readonly ApplicationDbContext _context;
        public DocumentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Documento>> GetDocumentosAsync()
        {
            return await _context.Documentos
                .Include(c => c.Proyecto) // Incluir el proyecto relacionado
                .ToListAsync();
        }

        public async Task<Documento?> GetDocumentoByIdAsync(int id)
        {
            return await _context.Documentos.FindAsync(id);
        }

        public async Task<Documento> CreateDocumentoAsync(Documento documento)
        {
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task<bool> UpdateDocumentoAsync(int id, Documento documento)
        {
            var documentoExistente = await _context.Documentos.FindAsync(id);
            if (documentoExistente == null) return false;

            documentoExistente.Nombre = documento.Nombre;
            documentoExistente.Enlace = documento.Enlace;
            documentoExistente.Tipo = documento.Tipo;
            documentoExistente.ProyectoId = documento.ProyectoId;

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
        public async Task<bool> DeleteDocumentoAsync(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null) return false;

            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
