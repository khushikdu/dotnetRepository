using Assignment_2.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assignment_2.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions and returning appropriate HTTP responses.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
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

        /// <summary>
        /// Handles the exception and returns an appropriate HTTP response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case InvalidCredentialsException:
                    status = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case UniqueEmailException:
                    status = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case UniqueUsernameException:
                    status = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var response = new { message };
            string payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(payload);
        }
    }
}
