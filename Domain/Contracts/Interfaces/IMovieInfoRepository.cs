using Domain.Models.Dtos.MovieDtos;
using Domain.Models.Entities;

namespace Domain.Contracts.Interfaces
{
    public interface IMovieInfoRepository : IRepositoryCommon<Movie>
    {
        // Task<IEnumerable<Movie>> GetMoviesAsync(GetMoviesQueryParamDTO paramDto);
        Task<Movie?> GetSingleMovieAsync(int Id, bool trackChanges = false);

        Task<Movie?> GetMovieDetailsAsync(int Id, bool trackChanges = false);

        Task<IEnumerable<Movie>> GetMoviesAsync(
            GetMoviesQueryParamDTO? paramDTO,
            bool trackChanges = false
        );

        Task<int> DeleteMovieAsync(int Id);

        Task AddMovieAsync(Movie movie);

        Task<bool> Exists(int Id);
    }
}
