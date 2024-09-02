using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieCardsAPI.CustomValidations
{
    public partial class ValidateRatingFormat : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            const string errorMessage =
                "Rating need to be a value (optionally with one decimal) in the range of 0 to 10";
            if (value is double input)
            {
                return
                    input <= 10.0
                    && input >= 0.0
                    //Check for 1 or 2 digits optionally followed by a "." plus maximally one more digit
                    && MyRegex().IsMatch(input.ToString().Replace(",", "."))
                    ? ValidationResult.Success
                    : new ValidationResult(errorMessage);
            }

            return new ValidationResult(errorMessage);
        }

        [GeneratedRegex(@"^\d{1,2}(\.\d)?$")]
        private static partial Regex MyRegex();
    }
}
