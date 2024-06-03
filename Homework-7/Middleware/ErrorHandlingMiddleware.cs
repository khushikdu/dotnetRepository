using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Homework_7.Middleware
{
    /// <summary>
    /// Middleware for handling errors.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor for ErrorHandlingMiddleware.
        /// </summary>
        /// <param name="next">Delegate representing the next middleware in the pipeline.</param>
        /// <param name="logger">Logger instance for logging.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Middleware implementation to handle errors.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        /// <returns>Task representing the asynchronous middleware operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Middleware: An unhandled exception occurred.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Middleware: An error occurred while processing the request.");
            }
        }
    }
}
