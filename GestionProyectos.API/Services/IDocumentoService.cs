using GestionProyectos.API.Models;

namespace GestionProyectos.API.Services
{
    public interface IDocumentoService
    {
        Task<IEnumerable<Documento>> GetDocumentosAsync();
        Task<Documento?> GetDocumentoByIdAsync(int id);
        Task<Documento> CreateDocumentoAsync(Documento documento);
        Task<bool> UpdateDocumentoAsync(int id, Documento documento);
        Task<bool> DeleteDocumentoAsync(int id);
    }
}
