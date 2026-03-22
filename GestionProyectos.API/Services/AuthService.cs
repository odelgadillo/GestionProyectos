using GestionProyectos.API.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace GestionProyectos.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null) return null;

            bool passwordValida = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);

            if(!passwordValida) return null;

            // TODO: Aquí se debería generar un token JWT real con la información del usuario y una clave secreta

            return "Token-Temporal-De-Prueba"; // Aquí se debería generar un token JWT real
        }
    }
}
