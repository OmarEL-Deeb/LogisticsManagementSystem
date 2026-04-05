using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Logistics.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // بنخلي الـ Request يكمل مساره العادي للـ Controllers
                await _next(context);
            }
            catch (Exception ex)
            {
                // لو حصل أي Exception في أي مكان في السيستم، بنصطاده هنا
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // بنفترض إن معظم الـ Exceptions اللي بتيجي من الـ Services هي Business Rules Violation (Bad Request 400)
            // طبعاً تقدر تزود Custom Exceptions زي NotFoundException وتديله Status 404
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // بنشكل الـ Response زي ما الدوكيومنت بتاعك طلب بالظبط
            var response = new
            {
                status = context.Response.StatusCode,
                message = exception.Message // دي الرسالة اللي كنا بنكتبها في الـ throw new Exception
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}