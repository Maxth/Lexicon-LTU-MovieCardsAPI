using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieCardsAPI.Constant;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;
using MovieCardsAPI.Services;

namespace MovieCardsAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMovieInfoRepository _movieInfoRepository;
        private readonly IMapper _mapper;

        public MoviesController(
            IRepository repository,
            IMovieInfoRepository movieInfoRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _movieInfoRepository = movieInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _movieInfoRepository.GetMoviesAsync();

            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetSingleMovie(int Id)
        {
            var movie = await _movieInfoRepository.GetSingleMovieAsync(Id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDTO>(movie));
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int Id)
        {
            var movie = await _movieInfoRepository.GetMovieDetailsAsync(Id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDetailsDTO>(movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int Id)
        {
            var movieToDelete = await _movieInfoRepository.GetSingleMovieAsync(Id);

            if (movieToDelete is null)
            {
                return NotFound();
            }

            _movieInfoRepository.DeleteMovie(movieToDelete);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieForCreationDTO movieForCreationDto)
        {
            var movie = _mapper.Map<Movie>(movieForCreationDto);
            _movieInfoRepository.AddMovie(movie);

            await _repository.SaveChangesAsync();

            return CreatedAtAction(
                "GetSingleMovie",
                new { id = movie.Id },
                _mapper.Map<MovieDTO>(movie)
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int Id, MovieForUpdateDTO movieForUpdateDTO)
        {
            var movie = await _movieInfoRepository.GetSingleMovieAsync(Id);

            if (movie is null)
            {
                return NotFound();
            }

            _mapper.Map(movieForUpdateDTO, movie);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchMovie(
            int Id,
            JsonPatchDocument<MovieForPatchDTO> jsonPatchDocument
        )
        {
            var movie = await _movieInfoRepository.GetSingleMovieAsync(Id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieToPatchDto = _mapper.Map<MovieForPatchDTO>(movie);

            jsonPatchDocument.ApplyTo(movieToPatchDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(movieToPatchDto))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(movieToPatchDto, movie);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
