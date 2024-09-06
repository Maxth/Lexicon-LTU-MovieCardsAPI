using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Service.Validation.Exceptions
{
    public class InvalidJsonPatchException : Exception
    {
        public ModelStateDictionary ModelState { get; }

        public InvalidJsonPatchException(ModelStateDictionary modelState)
        {
            ModelState = modelState;
        }
    }
}
