using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace PruebaTecnicaApi.Extensions
{
    /// <summary>
    ///     Exception Handler Middleware
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Extension to catch all unhandled exceptions
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        /// <typeparam name="T"></typeparam>
        public static void ConfigureExceptionHandler<T>(this IApplicationBuilder app, ILogger<T> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError("Something went wrong: {e}", contextFeature.Error);
                        await context.Response.WriteAsync("" ?? contextFeature.Error.Message);
                    }
                });
            });
        }
    }

}


