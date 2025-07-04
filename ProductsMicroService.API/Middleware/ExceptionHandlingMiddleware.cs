﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProductsMicroService.API.Middleware
{   
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Log the exception type and message 
                _logger.LogError($"{ex.GetType().ToString()} : {ex.Message}");

                if (ex.InnerException is not null)
                {
                    //log the inner exception type and message
                    _logger.LogError($"{ex.InnerException.GetType().ToString()} : {ex.InnerException.Message}");
                }

                httpContext.Response.StatusCode = 500; // Internal Server Error

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = ex.Message,
                    Type = ex.GetType().ToString()
                });

                throw;
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
