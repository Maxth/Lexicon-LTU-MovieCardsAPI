using Domain.Models.Dtos.ActorDtos;

namespace Domain.Models.Dtos.MovieDtos
{
    public class MovieDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public double? Rating { get; set; }
        public IEnumerable<ActorDTO>? Actors { get; set; }
    }
}
