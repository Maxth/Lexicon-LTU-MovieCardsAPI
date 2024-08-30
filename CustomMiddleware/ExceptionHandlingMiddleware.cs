using System.Data.Common;
using System.Net;
using EntityFramework.Exceptions.Common;
using MovieCardsAPI.Constant;

namespace MovieCardsAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                    if (ex.ConstraintName.Equals(Constants.UniqueMovieIndex))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.Conflict,
                            Constants.MovieExistErrMsg
                        );
                        break;
                    }
                    if (ex.ConstraintName.Equals(Constants.UniqueDirectorIndex))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.Conflict,
                            Constants.DirectorExistErrMsg
                        );
                        break;
                    }
                    break;
                case ReferenceConstraintException ex:
                    if (ex.ConstraintName.Equals(Constants.FK_MovieDirectorId))
                    {
                        response = new ExceptionResponse(
                            HttpStatusCode.UnprocessableEntity,
                            Constants.MovieDirectorFKErrMsg
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
