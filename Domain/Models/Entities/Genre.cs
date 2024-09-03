using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities
{
#nullable disable
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Movie> Movie { get; set; }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
