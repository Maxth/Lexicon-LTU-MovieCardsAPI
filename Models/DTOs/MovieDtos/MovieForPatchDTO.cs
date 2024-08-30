using System.ComponentModel.DataAnnotations;
using MovieCardsAPI.Constant;

namespace MovieCardsAPI.DTOs
{
    public class MovieForPatchDTO
    {
        [MaxLength(Constants.MovieTitleMaxLength)]
        public string? Title { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [MaxLength(Constants.MovieDescMaxLength)]
        public string? Description { get; set; }
    }
}
