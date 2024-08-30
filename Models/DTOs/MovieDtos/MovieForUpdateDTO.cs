using System.ComponentModel.DataAnnotations;
using MovieCardsAPI.Constant;

namespace MovieCardsAPI.DTOs
{
    public class MovieForUpdateDTO
    {
        [Required]
        [MaxLength(Constants.MovieTitleMaxLength)]
        public required string Title { get; set; }

        [Required]
        public DateOnly? ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [MaxLength(Constants.MovieDescMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int? DirectorId { get; set; }
    }
}
