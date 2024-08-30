using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.DTOs
{
    public class MovieForPatchDTO
    {
        [MaxLength(80)]
        public string? Title { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        [MaxLength(3)]
        public string? Rating { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
