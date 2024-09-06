using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace API.ModelBinding
{
    public class InvalidQueryParameterModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            var queryParams = bindingContext.HttpContext.Request.Query;

            // Get the properties of the model
            var modelProperties = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name);

            // Get the query string parameters
            var queryParamKeys = queryParams.Keys;

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
            }

            // Use the default binder for actual model binding
            var defaultBinder = new SimpleTypeModelBinder(modelType, new LoggerFactory());
            await defaultBinder.BindModelAsync(bindingContext);
        }
    }
}
