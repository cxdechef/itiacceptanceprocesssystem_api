using System.Net;
using Newtonsoft.Json;

namespace ITIAcceptanceProcessSystem.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message = "An unexpected error occurred. Please try again later.",
                details = exception.Message // For production, consider hiding the full exception details
            };

            var errorJson = JsonConvert.SerializeObject(errorResponse);

            return context.Response.WriteAsync(errorJson);
        }
    }
}
