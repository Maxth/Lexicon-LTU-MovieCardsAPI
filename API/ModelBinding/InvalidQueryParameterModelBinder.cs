using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Domain.Models.Dtos.MovieDtos;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace API.ModelBinding
{
    public class InvalidQueryParameterModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var defaultBinder = new SimpleTypeModelBinder(
                bindingContext.ModelType,
                new LoggerFactory()
            );

            // Get the query string parameters
            var queryParams = bindingContext.HttpContext.Request.Query;
            var queryParamKeys = queryParams.Keys;

            // Get the properties of the model

            var modelProperties = bindingContext
                .ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name);

            // Check for any query parameters that don't match the model's properties
            var invalidParams = queryParamKeys
                .Except(modelProperties, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (invalidParams.Count != 0)
            {
                // Handle misspelled parameters, e.g., log or add a model state error
                foreach (var invalidParam in invalidParams)
                {
                    bindingContext.ModelState.AddModelError(
                        invalidParam,
                        $"The query parameter '{invalidParam}' is not recognized."
                    );
                }

                await defaultBinder.BindModelAsync(bindingContext);
                return;
            }

            //Now we know that all the query params are correctly formed.
            //Now we do additional checks using custom validation attribute.
            var validationAttribute = new ValidateGetMoviesQueryParams();
            var validationContext = new ValidationContext(bindingContext.Model ?? string.Empty);
            var obj = new GetMoviesQueryParamDTO()
            {
                Title = queryParams["title"],
                ActorName = queryParams["actorName"],
                DirectorName = queryParams["directorName"],
                Genre = queryParams["genre"],
                IncludeActors = bool.TryParse(queryParams["includeActors"], out bool b) && b,
                ReleaseDateFrom = DateOnly.TryParse(queryParams["releaseDateFrom"], out DateOnly d)
                    ? d
                    : null,
                ReleaseDateTo = DateOnly.TryParse(queryParams["releaseDateTo"], out DateOnly d2)
                    ? d2
                    : null,
                SortBy = [.. queryParams["sortBy"]],
                SortOrder = queryParams["sortOrder"]
            };
            var validationResult = validationAttribute.GetValidationResult(obj, validationContext);

            if (validationResult != ValidationResult.Success)
            {
                bindingContext.ModelState.AddModelError(
                    "Query parameter validation error",
                    validationResult?.ErrorMessage ?? string.Empty
                );
            }

            await defaultBinder.BindModelAsync(bindingContext);

            // Use the default binder for actual model binding
        }
    }
}
