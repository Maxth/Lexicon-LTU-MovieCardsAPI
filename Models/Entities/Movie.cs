using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieCardsAPI.Constant;

namespace MovieCardsApi.Entities
#nullable disable
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [MaxLength(3)]
        public string Rating { get; set; }

        //Foreign key
        [ForeignKey(Constants.FK_MovieDirectorId)]
        public int DirectorId { get; set; }

        //Navigation props
        public Director Director { get; set; }
        public ICollection<Actor> Actor { get; set; }
        public ICollection<Genre> Genre { get; set; }
    }
}
