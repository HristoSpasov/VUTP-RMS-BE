namespace RMS.API.Infrastructure.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ErrorHandlingMiddleware> logger;

        private readonly IHostingEnvironment environment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IHostingEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, exception.Message, exception.StackTrace, exception.GetBaseException(), exception.GetBaseException()?.Message);

                await this.HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorMessage = "Something went wrong. Please contact our support team";
            if (exception is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = "Invalid request";
            }
            else if (exception is InvalidOperationException)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = exception.Message;
            }
            else if (exception is UnauthorizedAccessException || exception is SecurityTokenException)
            {
                code = HttpStatusCode.Unauthorized;
                errorMessage = "Invalid username or password";
            }

            if (this.environment.IsStaging())
            {
                errorMessage = exception.Message;
            }

            var result = JsonConvert.SerializeObject(new { error = errorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}