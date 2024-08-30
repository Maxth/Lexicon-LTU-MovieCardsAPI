using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.DTOs
{
    public class MovieForUpdateDTO
    {
        [Required]
        [MaxLength(80)]
        public required string Title { get; set; }

        [Required]
        public DateOnly? ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int? DirectorId { get; set; }
    }
}
