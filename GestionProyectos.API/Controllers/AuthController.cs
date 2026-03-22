using GestionProyectos.API.DTOs;
using GestionProyectos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionProyectos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            if (token == null) return Unauthorized("Credenciales incorrectas");

            return Ok(new { Token = token });
        }
    }
}
