using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionProyectos.API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "X-Api-Key"; // NOmbre de la cabecera
        private const string APIKEY = "MiClaveSecreta123"; // Contraseña de la API

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1. Verificar si la cabecera existe
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult() { StatusCode = 401, Content = "No se proporcionó la clave de API." };
                return;
            }

            // 2. Verificar si la clave es correcta
            if (!APIKEY.Equals(extractedApiKey))
            {
                context.Result = new ContentResult() { StatusCode = 403, Content = "Acceso no autorizado: API Key inválida" };
                return;
            }

            await next(); // Si la clave es correcta, continúa con la ejecución del controlador o acción
        }
    }
}
