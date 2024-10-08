using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Dtos.DirectorDtos
{
    public class DirectorForCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
