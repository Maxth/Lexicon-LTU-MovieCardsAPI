using Domain.Models.Dtos.MovieDtos;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.ModelBinding
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            // Apply this binder to your specific models (or to all models if you want)
            if (context.Metadata.ModelType == typeof(GetMoviesQueryParamDTO))
            {
                return new InvalidQueryParameterModelBinder();
            }
            return null;
        }
    }
}
