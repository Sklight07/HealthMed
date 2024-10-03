using System.Text.Json;

namespace HealthMed.Api.Configs
{
    public class CustomForbiddenResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomForbiddenResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Chama o próximo middleware
            await _next(context);

            // Verifica se a resposta é 403
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Você não tem permissão para acessar este recurso."
                };

                // Serializa o objeto para JSON
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
