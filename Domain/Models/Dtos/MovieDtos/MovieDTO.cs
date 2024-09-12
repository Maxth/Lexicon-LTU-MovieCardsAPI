using Domain.Models.Dtos.ActorDtos;
using Domain.Models.Dtos.DirectorDtos;
using Domain.Models.Dtos.GenreDtos;

namespace Domain.Models.Dtos.MovieDtos
{
    public class MovieDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public double? Rating { get; set; }
        public IEnumerable<ActorDTO>? Actors { get; set; }
        public IEnumerable<GenreDTO>? Genres { get; set; }
        public DirectorDTO? Director { get; set; }
    }
}
