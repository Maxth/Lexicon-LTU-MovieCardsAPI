using System.ComponentModel.DataAnnotations;
using MovieCardsAPI.Constant;
using MovieCardsAPI.CustomValidations;

namespace MovieCardsAPI.DTOs
{
    public class MovieForUpdateDTO
    {
        [Required]
        [MaxLength(Constants.MovieTitleMaxLength)]
        public required string Title { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [MaxLength(Constants.MovieDescMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int? DirectorId { get; set; }
    }
}
