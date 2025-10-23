using System.Net;
using System.Text.Json;

namespace CoinDispenser.Api.Middleware
{
    // Centralized exception handler that logs and returns sanitized JSON
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var payload = new { error = "An unexpected error occurred" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        }
    }
}