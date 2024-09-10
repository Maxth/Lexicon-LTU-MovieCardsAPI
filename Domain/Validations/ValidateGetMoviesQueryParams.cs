using System.ComponentModel.DataAnnotations;
using Domain.Models.Dtos.MovieDtos;

namespace Domain.Validations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ValidateGetMoviesQueryParams : ValidationAttribute
{
    private static readonly string[] validSortByValues = new[] { "rating", "releasedate", "title" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is GetMoviesQueryParamDTO paramDTO)
        {
            if (paramDTO?.SortBy?.Count == 0 && paramDTO.SortOrder != null)
            {
                return new ValidationResult(
                    "The query param sortOrder cannot be used without using a valid value of query param sortBy"
                );
            }

            if (paramDTO?.SortBy?.Any(p => !validSortByValues.Contains(p.ToLower())) ?? false)
            {
                return new ValidationResult(
                    "The query param sortBy must be either rating, releaseDate or title"
                );
            }
            if (
                paramDTO?.SortOrder != null
                && !paramDTO.SortOrder.Equals("ascending", StringComparison.OrdinalIgnoreCase)
                && !paramDTO.SortOrder.Equals("descending", StringComparison.OrdinalIgnoreCase)
            )
            {
                return new ValidationResult(
                    "The query param sortOrder must be either ascending or descending"
                );
            }

            if (paramDTO?.ReleaseDateTo != null && paramDTO.ReleaseDateFrom != null)
            {
                return paramDTO.ReleaseDateFrom <= paramDTO.ReleaseDateTo
                    ? ValidationResult.Success
                    : new ValidationResult(
                        "The query param fromDate need to be less than or equal to the query param toDate"
                    );
            }
        }

        return ValidationResult.Success;
    }
}
