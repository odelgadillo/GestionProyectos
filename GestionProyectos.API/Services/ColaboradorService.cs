using GestionProyectos.API.Data;
using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly ApplicationDbContext _context;
        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Colaborador>> GetColaboradoresAsync()
        {
            return await _context.Colaboradores
                .Include(c => c.Proyecto) // Incluir el proyecto relacionado
                .ToListAsync();
        }
        public async Task<Colaborador?> GetColaboradorByIdAsync(int id)
        {
            return await _context.Colaboradores.FindAsync(id);
        }
        public async Task<Colaborador> CreateColaboradorAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }
        public async Task<bool> UpdateColaboradorAsync(int id, Colaborador colaborador)
        {
            var colaboradorExistente = await _context.Colaboradores.FindAsync(id);
            if(colaboradorExistente == null) return false;

            colaboradorExistente.Nombre = colaborador.Nombre;
            colaboradorExistente.Rol = colaborador.Rol;
            colaboradorExistente.ProyectoId = colaborador.ProyectoId;

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
        public async Task<bool> DeleteColaboradorAsync(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null) return false;
            
            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}