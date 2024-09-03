namespace MovieCardsAPI.CustomValidations
{
    //     [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    //     public class ValidateGetMoviesQueryParams : ValidationAttribute
    //     {
    //         protected override ValidationResult? IsValid(object? value, ValidationContext _)
    //         {
    //             if (value is GetMoviesQueryParamDTO paramDto)
    //             {
    //                 if (paramDto.FromDate is not null && paramDto.ToDate is not null)
    //                 {
    //                     return paramDto.FromDate <= paramDto.ToDate
    //                         ? ValidationResult.Success
    //                         : new ValidationResult(
    //                             "Parameter fromDate need to be an earlier date than parameter toDate."
    //                         );
    //                 }
    //             }

    //             return ValidationResult.Success;
    //         }
    //     }
    // }
}
