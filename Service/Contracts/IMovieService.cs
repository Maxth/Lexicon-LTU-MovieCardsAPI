using Infrastructure.Dtos.MovieDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Service.Contracts;

public interface IMovieService
{
    Task<MovieDTO> GetSingleMovieAsync(int Id, bool trackChanges = false);

    Task<MovieDetailsDTO> GetMovieDetailsAsync(int Id, bool trackChanges = false);

    Task<IEnumerable<MovieDTO>> GetMoviesAsync(bool trackChanges = false);

    Task<int> DeleteMovie(int Id);

    Task<MovieDTO> AddMovie(MovieForCreationDTO inputDto);

    Task UpdateMovie(int Id, MovieForUpdateDTO inputDto);

    Task<MovieForPatchDTO> PatchMovie(int Id, JsonPatchDocument<MovieForPatchDTO> patchDoc);
}
