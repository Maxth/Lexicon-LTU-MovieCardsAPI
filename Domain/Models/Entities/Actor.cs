using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities
{
#nullable disable
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Movie> Movie { get; set; }
    }
}
