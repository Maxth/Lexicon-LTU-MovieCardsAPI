using Domain.Models.Entities;

namespace Domain.Contracts.Interfaces
{
    public interface IMovieInfoRepository : IRepositoryCommon<Movie>
    {
        // Task<IEnumerable<Movie>> GetMoviesAsync(GetMoviesQueryParamDTO paramDto);
        Task<Movie?> GetSingleMovieAsync(int Id, bool trackChanges = false);

        Task<Movie?> GetMovieDetailsAsync(int Id, bool trackChanges = false);

        Task<IEnumerable<Movie>> GetMoviesAsync(bool trackChanges = false);

        Task<int> DeleteMovie(int Id);

        Task AddMovie(Movie movie);

        Task<bool> Exists(int Id);
    }
}
