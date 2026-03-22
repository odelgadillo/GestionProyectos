namespace GestionProyectos.API.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        // Se retorna el token JWT si la autenticación es exitosa
    }
}
