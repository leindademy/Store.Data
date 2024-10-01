using Store.Service.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Store.Web.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExeptionMiddleware> _logger;

        public ExeptionMiddleware(RequestDelegate next,
            IHostEnvironment environment, 
            ILogger <ExeptionMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) //handle exception --> Middleware
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
                var response = _environment.IsDevelopment()
                    ? new CustomExeption((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomExeption((int) HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // Optional --> Additional form of beauty

                var json = JsonSerializer.Serialize(response, options); 

                await context.Response.WriteAsync(json);
            }
        }
    }
}
