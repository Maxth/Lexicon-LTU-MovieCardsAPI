using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IDirectorInfoRepository _directorInfoRepository;
        private readonly IMapper _mapper;

        public MoviesController(
            IRepository repository,
            IMovieInfoRepository movieInfoRepository,
            IDirectorInfoRepository directorInfoRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _movieInfoRepository = movieInfoRepository;
            _directorInfoRepository = directorInfoRepository;
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

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDTO>(movie));
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int Id)
        {
            var movie = await _movieInfoRepository.GetMovieDetailsAsync(Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDetailsDTO>(movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int Id)
        {
            var movieToDelete = await _movieInfoRepository.GetSingleMovieAsync(Id);

            if (movieToDelete == null)
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
            if (
                await _movieInfoRepository.MovieWithTitleAndReleaseDateExistsAsync(
                    movieForCreationDto.Title,
                    movieForCreationDto.ReleaseDate
                )
            )
            {
                return Conflict("A movie with that title and releasedate already exists");
            }

            if (!await _directorInfoRepository.DirectorExistsAsync(movieForCreationDto.DirectorId))
            {
                return BadRequest("There is no director with that Id. Add the director first.");
            }

            var movie = _mapper.Map<Movie>(movieForCreationDto);
            _movieInfoRepository.AddMovie(movie);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(
                "GetSingleMovie",
                new { id = movie.Id },
                _mapper.Map<MovieDTO>(movie)
            );
        }

        // [HttpPut("{id}")]
        // public async Task<ActionResult> UpdateMovie(int Id, MovieForUpdateDTO movieForUpdateDTO)
        // {
        //     if (await TitleAndReleaseDateExist(movieForUpdateDTO))
        //     {
        //         return Conflict("A movie with that title and releasedate already exists");
        //     }

        //     if (
        //         await _context
        //             .Director.Where(d => d.Id == movieForUpdateDTO.DirectorId)
        //             .FirstOrDefaultAsync() == null
        //     )
        //     {
        //         return BadRequest("There is no director with that Id");
        //     }

        //     var movie = new Movie(movieForUpdateDTO.Title)
        //     {
        //         Id = Id,
        //         ReleaseDate = movieForUpdateDTO.ReleaseDate,
        //         Rating = movieForUpdateDTO.Rating,
        //         Description = movieForUpdateDTO.Description,
        //         DirectorId = movieForUpdateDTO.DirectorId,
        //     };

        //     _context.Entry(movie).State = EntityState.Modified;
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private async Task<bool> TitleAndReleaseDateExist(IMovieCreationOrUpdateDto movieDto)
        // {
        //     return await _context.Movie.AnyAsync(m =>
        //         String.Equals(m.Title.ToLower(), movieDto.Title.ToLower())
        //         && m.ReleaseDate == movieDto.ReleaseDate
        //     );
        // }

        // private async Task<Director?> FindDirector(MovieForCreationDTO movieDto)
        // {
        //     return await _context.Director.FirstOrDefaultAsync(d =>
        //         string.Equals(d.Name.ToLower(), movieDto.Director.Name.ToLower())
        //         && d.DateOfBirth == movieDto.Director.DateOfBirth
        //     );
        // }
    }
}
