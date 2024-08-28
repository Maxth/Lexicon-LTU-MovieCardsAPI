using System.ComponentModel.DataAnnotations;
using MovieCards.Interfaces;

namespace MovieCardsAPI.DTOs
{
    public class MovieForUpdateDTO : IMovieCreationOrUpdateDto
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int DirectorId { get; set; }

        public MovieForUpdateDTO(string title)
        {
            Title = title;
        }
    }
}
