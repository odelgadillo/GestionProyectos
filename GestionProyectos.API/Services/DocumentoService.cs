using GestionProyectos.API.Data;
using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public async Task<Documento> CreateDocumentoAsync(int proyectoId, int usuarioId, IFormFile archivo)
        {
            // 1. Validar si el usuario es miembro del proyecto
            var esMiembro = await _context.AsignacionesProyectos
                .AnyAsync(a => a.ProyectoId == proyectoId && a.UsuarioId == usuarioId);

            if (!esMiembro) throw new UnauthorizedAccessException("El usuario no es miembro del proyecto.");

            // 2. Definir donde se guarda el archivo fisicamente
            string nombreArchivo = Guid.NewGuid().ToString() + "_" + archivo.FileName; // Asignamos un GUID para evitar duplicados
            string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if(!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);
            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            // 3. Guardar el archivo en el servidor
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // 4. Crear el registro en la base de datos
            var nuevoDocumento = new Documento
            {
                Nombre = archivo.FileName,
                Enlace = "/uploads/" + nombreArchivo, // Ruta relativa para acceder al archivo
                Tipo = DeterminarTipo(archivo.FileName),
                ProyectoId = proyectoId,
                UsuarioId = usuarioId,
                FechaSubida = DateTime.UtcNow
            };

            _context.Documentos.Add(nuevoDocumento);
            await _context.SaveChangesAsync();
            return nuevoDocumento;
        }

        private string DeterminarTipo(string nombreArchivo)
        {
            var extension = Path.GetExtension(nombreArchivo).ToLower();
            return extension switch
            {
                ".pdf" => "PDF",
                ".xlsx" or ".xls" => "Excel",
                ".docx" or ".doc" => "Word",
                _ => "URL"
            };
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
