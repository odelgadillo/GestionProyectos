using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Net;
using System.Text.Json;

namespace GestionProyectos.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continúa el flujo normal
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un error no controlado.");
                await HandleExceptionAsync(context, ex) ;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno en el servidor de Gestión de Proyectos.",
                Detailed = exception.Message // En produccion, esto se deberia ocultar
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}
