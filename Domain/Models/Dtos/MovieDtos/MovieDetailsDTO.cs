using Domain.Models.Dtos.ActorDtos;
using Domain.Models.Dtos.DirectorDtos;
using Domain.Models.Dtos.GenreDtos;

namespace Domain.Models.Dtos.MovieDtos
{
#nullable disable
    public class MovieDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public double? Rating { get; set; }
        public IEnumerable<GenreDTO> Genres { get; set; }
        public IEnumerable<ActorDTO> Actors { get; set; }
        public DirectorForMovieDetailsDTO Director { get; set; }
        public string Description { get; set; }
    }
}
