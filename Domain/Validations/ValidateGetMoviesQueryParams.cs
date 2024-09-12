using System.ComponentModel.DataAnnotations;
using Domain.Models.Dtos.MovieDtos;

namespace Domain.Validations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidateGetMoviesQueryParams : ValidationAttribute
{
    private static readonly string[] validSortByValues = new[]
    {
        "rating",
        "rating_desc",
        "releasedate",
        "releasedate_desc",
        "title",
        "title_desc"
    };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is GetMoviesQueryParamDTO paramDTO)
        {
            if (paramDTO?.SortBy?.Any(p => !validSortByValues.Contains(p.ToLower())) ?? false)
            {
                return new ValidationResult(
                    $"The query param sortBy must be either {string.Join(", ", validSortByValues)}"
                );
            }
            if (
                paramDTO?.SortBy?.Where(s => s.Contains("rating")).Count() > 1
                || paramDTO?.SortBy?.Where(s => s.Contains("releasedate")).Count() > 1
                || paramDTO?.SortBy?.Where(s => s.Contains("title")).Count() > 1
            )
            {
                return new ValidationResult(
                    "Invalid use of sortBy parameter. Must not use both the ascending and the descending variant of the same property."
                );
            }

            if (
                paramDTO?.ReleaseDateTo != null
                && paramDTO.ReleaseDateFrom != null
                && paramDTO.ReleaseDateFrom > paramDTO.ReleaseDateTo
            )
            {
                return new ValidationResult(
                    "The query param fromDate need to be less than or equal to the query param toDate"
                );
            }
        }

        return ValidationResult.Success;
    }
}
