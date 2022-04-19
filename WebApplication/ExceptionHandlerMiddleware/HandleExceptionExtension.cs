using Microsoft.AspNetCore.Builder;

namespace WebApplication.ExceptionHandlerMiddleware
{
    public static class HandleExceptionExtension
    {
        public static IApplicationBuilder UseHandleException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleExceptionMiddleware>();
        }

    }
}