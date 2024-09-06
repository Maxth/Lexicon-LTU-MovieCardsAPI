using Infrastructure.Dtos.MovieDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Service.Contracts;

public interface IMovieService
{
    Task<MovieDTO> GetSingleMovieAsync(int Id, bool trackChanges = false);

    Task<MovieDetailsDTO> GetMovieDetailsAsync(int Id, bool trackChanges = false);

    Task<IEnumerable<MovieDTO>> GetMoviesAsync(bool trackChanges = false);

    Task<int> DeleteMovieAsync(int Id);

    Task<MovieDTO> AddMovieAsync(MovieForCreationDTO inputDto);

    Task UpdateMovie(int Id, MovieForUpdateDTO inputDto);

    Task PatchMovieAsync(
        int Id,
        JsonPatchDocument<MovieForPatchDTO> patchDoc,
        ModelStateDictionary ModelState,
        Func<object, bool> TryValidateModel
    );
}
