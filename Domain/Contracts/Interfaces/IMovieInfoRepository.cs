using Domain.Models.Entities;

namespace Domain.Contracts.Interfaces
{
    public interface IMovieInfoRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();

        // Task<IEnumerable<Movie>> GetMoviesAsync(GetMoviesQueryParamDTO paramDto);
        Task<Movie?> GetSingleMovieAsync(int Id);

        Task<Movie?> GetMovieDetailsAsync(int Id);

        void DeleteMovie(Movie movie);

        void AddMovie(Movie movie);
    }
}
