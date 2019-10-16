namespace RMS.API.Infrastructure.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SwaggerAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IHttpContextAccessor ctxAccessor;

        private readonly List<string> ipWhitelist;

        public SwaggerAuthorizationMiddleware(RequestDelegate next, IHttpContextAccessor ctxAccessor, List<string> ipWhitelist)
        {
            this.next = next;
            this.ctxAccessor = ctxAccessor;
            this.ipWhitelist = ipWhitelist;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger")
                && !this.IsAuthenticated())
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await this.next.Invoke(context);
        }

        private string GetRequestIp()
        {
            var requestIp = this.ctxAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return requestIp;
        }

        private bool IsAuthenticated()
        {
            if (this.ipWhitelist.Any(authIp => authIp == this.GetRequestIp()))
            {
                return true;
            }

            return false;
        }
    }
}