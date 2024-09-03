using System.Net;
using Domain.Constants;
using EntityFramework.Exceptions.Common;

namespace Presentation.CustomMiddleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
            ExceptionResponse? response = null;

            switch (exception)
            {
                case UniqueConstraintException ex:
                    if (ex.ConstraintName.Equals(ConstVars.UniqueMovieIndex))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.Conflict,
                            ConstVars.MovieExistErrMsg
                        );
                        break;
                    }
                    if (ex.ConstraintName.Equals(ConstVars.UniqueDirectorIndex))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.Conflict,
                            ConstVars.DirectorExistErrMsg
                        );
                        break;
                    }
                    if (ex.ConstraintName.Equals(ConstVars.UniqueActorIndex))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.Conflict,
                            ConstVars.ActorExistErrMsg
                        );
                        break;
                    }
                    break;
                case ReferenceConstraintException ex:
                    if (ex.ConstraintName.Equals(ConstVars.FK_MovieDirectorId))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.UnprocessableEntity,
                            ConstVars.MovieDirectorFKErrMsg
                        );
                    }
                    break;
                default:
                    break;
            }

            if (response is not null)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
            else
            {
                throw exception;
            }
        }

        private record ExceptionResponse(HttpStatusCode StatusCode, string Description);
    }
}
