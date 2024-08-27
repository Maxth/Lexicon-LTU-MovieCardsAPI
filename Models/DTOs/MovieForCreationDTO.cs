using System.ComponentModel.DataAnnotations;
using MovieCards.Interfaces;

namespace MovieCardsAPI.DTOs
{
    public class MovieForCreationDTO : IMovieCreationOrUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [MaxLength(3)]
        public string Rating { get; set; } = string.Empty;
        public DirectorForCreationDTO Director { get; set; } = new DirectorForCreationDTO();

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
