using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace WebApplication.ExceptionHandlerMiddleware
{
    public class HandleExceptionMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger<HandleExceptionMiddleware> _logger;
        public HandleExceptionMiddleware(RequestDelegate next, ILogger<HandleExceptionMiddleware> logger)
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
            catch (Exception exception)
            {                
                await context.Response.WriteAsync($"Exception: {exception.Message}");
                _logger.LogError(exception.Message);
            }
        }
    }
}