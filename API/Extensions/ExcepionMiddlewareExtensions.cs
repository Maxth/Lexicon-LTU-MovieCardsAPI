using System.Net;
using Domain.Constants;
using Domain.Exceptions.NotFound;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Extensions
{
    public static class ExcepionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async httpContext =>
                {
                    var exceptionHandler = httpContext.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandler != null)
                    {
                        var factory = app.Services.GetRequiredService<ProblemDetailsFactory>();
                        await SendResponse(httpContext, factory, exceptionHandler.Error);
                    }
                });
            });
        }

        private static async Task SendResponse(
            HttpContext httpContext,
            ProblemDetailsFactory factory,
            Exception error
        )
        {
            ProblemDetails problemDetails;
            int statusCode;
            switch (error)
            {
                case DirectorNotFoundException ex:
                    statusCode = StatusCodes.Status404NotFound;
                    problemDetails = factory.CreateProblemDetails(
                        httpContext,
                        statusCode,
                        title: ex.Title,
                        detail: ex.Message
                    );

                    break;
                case MovieNotFoundException ex:
                    statusCode = StatusCodes.Status404NotFound;
                    problemDetails = factory.CreateProblemDetails(
                        httpContext,
                        statusCode,
                        title: ex.Title,
                        detail: ex.Message
                    );

                    break;
                case UniqueConstraintException ex:
                    statusCode = StatusCodes.Status409Conflict;
                    problemDetails = ex.ConstraintName switch
                    {
                        ConstVars.UniqueMovieIndex
                            => factory.CreateProblemDetails(
                                httpContext,
                                statusCode,
                                title: "Conflict",
                                detail: $"A movie with that title already exist"
                            ),
                        ConstVars.UniqueDirectorIndex
                            => factory.CreateProblemDetails(
                                httpContext,
                                statusCode,
                                title: "Conflict",
                                detail: "A director with that name and birthdate already exist"
                            ),
                        ConstVars.UniqueActorIndex
                            => factory.CreateProblemDetails(
                                httpContext,
                                statusCode,
                                title: "Conflict",
                                detail: "An actor with that name and birthdate already exist"
                            ),
                        _ => throw ex,
                    };
                    break;
                case ReferenceConstraintException ex:
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    problemDetails = ex.ConstraintName switch
                    {
                        ConstVars.FK_MovieDirectorId
                            => factory.CreateProblemDetails(
                                httpContext,
                                statusCode,
                                title: "Unprocessable Entity",
                                detail: "There is no director with that Id"
                            ),
                        _ => throw ex,
                    };
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    problemDetails = factory.CreateProblemDetails(
                        httpContext,
                        statusCode,
                        title: "Internal Server Error",
                        detail: error.Message
                    );
                    break;
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
