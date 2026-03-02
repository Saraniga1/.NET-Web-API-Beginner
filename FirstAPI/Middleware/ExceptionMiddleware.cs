using Azure;
using System.Net;
using System.Text.Json;

namespace FirstAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var responce = new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error. Please try again later.",
                    Detailed= ex.Message   
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(responce));
            }
        }
    }
}
