using System.ComponentModel.DataAnnotations;

namespace MovieCardsApi.Entities
{
#nullable disable
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Movie> Movie { get; set; }
        public ContactInformation ContactInformation { get; set; }
    }
}
