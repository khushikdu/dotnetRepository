using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Homework_7.Middleware
{
    /// <summary>
    /// Middleware for handling errors using the IMiddleware interface.
    /// </summary>
    public class ErrorHandlingIMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingIMiddleware> _logger;

        /// <summary>
        /// Constructor for ErrorHandlingIMiddleware.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        public ErrorHandlingIMiddleware(ILogger<ErrorHandlingIMiddleware> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Middleware implementation to handle errors.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        /// <param name="next">Delegate representing the next middleware in the pipeline.</param>
        /// <returns>Task representing the asynchronous middleware operation.</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("IMiddleware: An error occurred while processing the request.");
            }
        }
    }
}
