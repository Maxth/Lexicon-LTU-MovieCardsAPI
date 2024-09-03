using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Constants;

namespace Domain.Models.Entities
#nullable disable
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ConstVars.MovieTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [MaxLength(ConstVars.MovieDescMaxLength)]
        public string Description { get; set; }

        public double? Rating { get; set; }

        //Foreign key
        [ForeignKey(ConstVars.FK_MovieDirectorId)]
        public int DirectorId { get; set; }

        //Navigation props
        public Director Director { get; set; }
        public ICollection<Actor> Actor { get; set; }
        public ICollection<Genre> Genre { get; set; }
    }
}
