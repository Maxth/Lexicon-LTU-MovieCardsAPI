using MovieCardsApi.Entities;

namespace MovieCardsAPI.Services
{
    public interface IMovieInfoRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();

        Task<Movie?> GetSingleMovieAsync(int Id);

        Task<Movie?> GetMovieDetailsAsync(int Id);

        void DeleteMovie(Movie movie);

        Task<bool> MovieExistsAsync(int Id);

        Task SaveChangesAsync();
    }
}
