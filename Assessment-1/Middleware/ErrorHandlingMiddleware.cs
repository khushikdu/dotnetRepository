using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Assignment_2.CustomExceptions;
using Assessment_1.CustomExceptions;

namespace Assignment_2.Middlewares
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
                
                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest;
                    message = "User with same name already exists";
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
