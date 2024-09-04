using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Validations;

namespace Infrastructure.Dtos.MovieDtos
{
#nullable disable
    public class MovieForCreationDTO
    {
        [Required]
        [MaxLength(ConstVars.MovieTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [Required]
        public int? DirectorId { get; set; }

        [MaxLength(ConstVars.MovieDescMaxLength)]
        public string Description { get; set; }
    }
}
