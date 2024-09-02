using System.ComponentModel.DataAnnotations;
using MovieCardsAPI.Constant;
using MovieCardsAPI.CustomValidations;

namespace MovieCardsAPI.DTOs
{
    public class MovieForPatchDTO
    {
        [MaxLength(Constants.MovieTitleMaxLength)]
        [Required]
        public required string Title { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [MaxLength(Constants.MovieDescMaxLength)]
        public string? Description { get; set; }
    }
}
