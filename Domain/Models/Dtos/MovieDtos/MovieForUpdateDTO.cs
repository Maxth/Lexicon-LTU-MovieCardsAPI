using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Validations;

namespace Domain.Models.Dtos.MovieDtos
{
    public class MovieForUpdateDTO
    {
        [Required]
        [MaxLength(ConstVars.MovieTitleMaxLength)]
        public required string Title { get; set; }

        [ValidateRatingFormat]
        public double? Rating { get; set; }

        [MaxLength(ConstVars.MovieDescMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int? DirectorId { get; set; }
    }
}
