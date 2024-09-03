using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.Validations
{
    public partial class ValidateRatingFormat : ValidationAttribute
    {
        [GeneratedRegex(@"^\d{1,2}(\.\d)?$")]
        private static partial Regex _regex();

        private readonly NumberFormatInfo _nfi = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            const string errorMessage =
                "Rating need to be a value (optionally with one decimal) in the range of 0 to 10";
            if (value is double input)
            {
                return
                    input <= 10.0
                    && input >= 0.0
                    //Check for 1 or 2 digits optionally followed by a dot plus maximally one more digit
                    && _regex().IsMatch(input.ToString(_nfi))
                    ? ValidationResult.Success
                    : new ValidationResult(errorMessage);
            }

            return new ValidationResult(errorMessage);
        }
    }
}
