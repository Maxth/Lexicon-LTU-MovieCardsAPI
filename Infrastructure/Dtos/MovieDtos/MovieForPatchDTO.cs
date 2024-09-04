using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Validations;

namespace Infrastructure.Dtos.MovieDtos
{
    public class MovieForPatchDTO
    {
        [MaxLength(ConstVars.MovieTitleMaxLength)]
        [Required]
        public required string Title { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [MaxLength(ConstVars.MovieDescMaxLength)]
        public string? Description { get; set; }
    }
}
