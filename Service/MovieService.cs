using AutoMapper;
using Domain.Contracts.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Dtos.MovieDtos;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;

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

    public async Task<MovieDTO> AddMovie(MovieForCreationDTO inputDto)
    {
        var movie = _mapper.Map<Movie>(inputDto);
        await _rm.MovieInfoRepository.AddMovie(movie);
        return _mapper.Map<MovieDTO>(movie);
    }

    public async Task<int> DeleteMovie(int Id)
    {
        if (!await _rm.MovieInfoRepository.Exists(Id))
        {
            //FIXME
            throw new NotImplementedException();
        }

        var rowsAffected = await _rm.MovieInfoRepository.DeleteMovie(Id);
        if (rowsAffected != 1)
        {
            //FIXME
            throw new NotImplementedException();
        }
        return rowsAffected;
    }

    public async Task<MovieDetailsDTO> GetMovieDetailsAsync(int Id, bool trackChanges = false)
    {
        var movieWithDetails = await _rm.MovieInfoRepository.GetMovieDetailsAsync(Id, trackChanges);

        if (movieWithDetails is null)
        {
            //FIXME
            throw new NotImplementedException();
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
            //FIXME
            throw new NotImplementedException();
        }

        return _mapper.Map<MovieDTO>(movie);
    }

    public async Task<MovieForPatchDTO> PatchMovie(int Id, JsonPatchDocument patchDoc)
    {
        if (patchDoc is null)
        {
            //FIXME
            throw new NotImplementedException();
        }

        var movie = await _rm.MovieInfoRepository.GetSingleMovieAsync(Id, trackChanges: true);

        if (movie is null)
        {
            //FIXME
            throw new NotImplementedException();
        }

        var movieForPatchDto = _mapper.Map<MovieForPatchDTO>(movie);

        patchDoc.ApplyTo(movieForPatchDto);

        return movieForPatchDto;
    }

    public async Task UpdateMovie(int Id, MovieForUpdateDTO inputDto)
    {
        var movieToUpdate = await _rm.MovieInfoRepository.GetSingleMovieAsync(
            Id,
            trackChanges: true
        );

        if (movieToUpdate is null)
        {
            //FIXME
            throw new NotImplementedException();
        }

        _mapper.Map(inputDto, movieToUpdate);
    }
}
