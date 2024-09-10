using AutoMapper;
using Domain.Contracts.Interfaces;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Models.Dtos.MovieDtos;
using Domain.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Contracts;
using Service.Validation.Exceptions;

namespace Service;

public class MovieService : IMovieService
{
    private IRepositoryManager _rm;
    private IMapper _mapper;

    public MovieService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _rm = repositoryManager;
        _mapper = mapper;
    }

    public async Task<MovieDTO> AddMovieAsync(MovieForCreationDTO inputDto)
    {
        var movie = _mapper.Map<Movie>(inputDto);
        await _rm.MovieInfoRepository.AddMovieAsync(movie);
        await _rm.CompleteAsync();
        return _mapper.Map<MovieDTO>(movie);
    }

    public async Task<int> DeleteMovieAsync(int Id)
    {
        if (!await _rm.MovieInfoRepository.Exists(Id))
        {
            throw new MovieNotFoundException(Id);
        }
        var rowsAffected = await _rm.MovieInfoRepository.DeleteMovieAsync(Id);
        if (rowsAffected != 1)
        {
            //FIXME
            throw new NotImplementedException($"Rows deleted {rowsAffected}. Expected: 1");
        }
        return rowsAffected;
    }

    public async Task<MovieDetailsDTO> GetMovieDetailsAsync(int Id, bool trackChanges = false)
    {
        var movieWithDetails = await _rm.MovieInfoRepository.GetMovieDetailsAsync(Id, trackChanges);

        if (movieWithDetails is null)
        {
            throw new MovieNotFoundException(Id);
        }

        return _mapper.Map<MovieDetailsDTO>(movieWithDetails);
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesAsync(bool trackChanges = false)
    {
        var movies = await _rm.MovieInfoRepository.GetMoviesAsync(trackChanges);
        return _mapper.Map<IEnumerable<MovieDTO>>(movies);
    }

    public async Task<MovieDTO> GetSingleMovieAsync(int Id, bool trackChanges = false)
    {
        var movie = await _rm.MovieInfoRepository.GetSingleMovieAsync(Id, trackChanges);

        if (movie is null)
        {
            throw new MovieNotFoundException(Id);
        }

        return _mapper.Map<MovieDTO>(movie);
    }

    public async Task PatchMovieAsync(
        int Id,
        JsonPatchDocument<MovieForPatchDTO> patchDoc,
        ModelStateDictionary ModelState,
        Func<object, bool> TryValidateModel
    )
    {
        if (patchDoc is null)
        {
            throw new NoJsonPatchException();
        }

        var movie = await _rm.MovieInfoRepository.GetSingleMovieAsync(Id, trackChanges: true);

        if (movie is null)
        {
            throw new MovieNotFoundException(Id);
        }

        var movieForPatchDto = _mapper.Map<MovieForPatchDTO>(movie);

        patchDoc.ApplyTo(movieForPatchDto, ModelState);
        TryValidateModel(movieForPatchDto);
        if (!ModelState.IsValid)
        {
            throw new InvalidJsonPatchException(ModelState);
        }

        _mapper.Map(movieForPatchDto, movie);
        await _rm.CompleteAsync();
    }

    public async Task UpdateMovie(int Id, MovieForUpdateDTO inputDto)
    {
        var movieToUpdate = await _rm.MovieInfoRepository.GetSingleMovieAsync(
            Id,
            trackChanges: true
        );

        if (movieToUpdate is null)
        {
            throw new MovieNotFoundException(Id);
        }
        _mapper.Map(inputDto, movieToUpdate);
        await _rm.CompleteAsync();
    }
}
