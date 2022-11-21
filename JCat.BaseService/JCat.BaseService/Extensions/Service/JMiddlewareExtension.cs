using JCat.BaseService.Middleware;
using Microsoft.AspNetCore.Builder;

namespace JCat.BaseService.Extensions.Service
{
    public static class JMiddlewareExtension
    {
        public static void AddBaseMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<JBaseMiddleware>();
        }
    }
}
