using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieCardsAPI.DTOs
{
#nullable disable
    public class MovieForCreationDTO
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public DateOnly? ReleaseDate { get; set; }

        [MaxLength(3)]
        public string Rating { get; set; }

        [Required]
        public int? DirectorId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
