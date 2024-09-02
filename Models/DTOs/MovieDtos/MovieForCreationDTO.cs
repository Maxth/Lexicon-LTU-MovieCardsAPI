using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieCardsAPI.Constant;
using MovieCardsAPI.CustomValidations;

namespace MovieCardsAPI.DTOs
{
#nullable disable
    public class MovieForCreationDTO
    {
        [Required]
        [MaxLength(Constants.MovieTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [Required]
        public int? DirectorId { get; set; }

        [MaxLength(Constants.MovieDescMaxLength)]
        public string Description { get; set; }
    }
}
