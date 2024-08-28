using System.ComponentModel.DataAnnotations;
using MovieCards.Interfaces;

namespace MovieCardsAPI.DTOs
{
    public class MovieForCreationDTO : IMovieCreationOrUpdateDto
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [Required]
        public DirectorForCreationDTO Director { get; set; } = new DirectorForCreationDTO();

        [MaxLength(200)]
        public string? Description { get; set; }

        public MovieForCreationDTO(string title)
        {
            Title = title;
        }
    }
}
