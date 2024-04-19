using CS_Readings_Guide.Helper;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CS_Readings_Guide.ErrorHandlingMiddleware
{
    public class ErrorHandlingMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        public IWebHostEnvironment _env { get; }
        #endregion
        #region Contructor

        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        #endregion
        #region HandleFunctions
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Logging the execption into the Database
                //Log.Error(ex, ex.Message, context.Request, "");
                await HandleExceptionAsync(context, ex,_env);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment _env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                // Show Details only to The Users 
            };

            if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "Unauthorized access.";
            }

            Console.WriteLine($"An unhandled exception occurred: {exception}");

            // If development mode, include exception details
            if (_env.IsDevelopment())
            {
                errorResponse.ExceptionDetails = exception.Message+"=>>"+exception.InnerException;
            }

            // Serialize the error response to JSON and write it to the response body
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(jsonErrorResponse);
        }
        #endregion
    }
}
