//Middlewares/ErrorHandlerMiddleware.cs
using MyWebApi.Helpers;
using System.Net;
using Newtonsoft.Json;

namespace MyWebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // TODO: Depending on the exception type, you might want to set other status codes.

            var result = new ApiResponse<string>
            {
                StatusCode = (int)code,
                Error = exception.Message
            };
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
