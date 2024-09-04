using System.ComponentModel.DataAnnotations.Schema;
using Domain.Constants;

namespace Domain.Models.Entities.Joins
{
#nullable disable
    public class ActorMovie
    {
        public int MovieId { get; set; }

        public int ActorId { get; set; }

        public Movie Movie { get; set; }

        public Actor Actor { get; set; }
    }
}
