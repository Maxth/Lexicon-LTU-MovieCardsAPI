using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCards.Interfaces;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;
using MovieCardsAPI.Services;

namespace MovieCardsAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieInfoRepository __movieInfoRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieInfoRepository movieInfoRepository, IMapper mapper)
        {
            __movieInfoRepository = movieInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await __movieInfoRepository.GetMoviesAsync();

            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(movies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetSingleMovie(int Id)
        {
            var movie = await __movieInfoRepository.GetSingleMovieAsync(Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDTO>(movie));
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int Id)
        {
            var movie = await __movieInfoRepository.GetMovieDetailsAsync(Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieDetailsDTO>(movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int Id)
        {
            var movieToDelete = await __movieInfoRepository.GetSingleMovieAsync(Id);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            __movieInfoRepository.DeleteMovie(movieToDelete);

            await __movieInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        // [HttpPost]
        // public async Task<ActionResult> AddMovie(MovieForCreationDTO movieForCreationDto)
        // {
        //     if (await TitleAndReleaseDateExist(movieForCreationDto))
        //     {
        //         return Conflict("A movie with that title and releasedate already exists");
        //     }

        //     Director? director = await FindDirector(movieForCreationDto);

        //     if (director == null)
        //     {
        //         director = new Director(movieForCreationDto.Director.Name)
        //         {
        //             DateOfBirth = movieForCreationDto.Director.DateOfBirth
        //         };

        //         _context.Director.Add(director);
        //         await _context.SaveChangesAsync();
        //     }

        //     var movie = new Movie(movieForCreationDto.Title)
        //     {
        //         ReleaseDate = movieForCreationDto.ReleaseDate,
        //         Description = movieForCreationDto.Description,
        //         Rating = movieForCreationDto.Rating,
        //         DirectorId = director.Id,
        //     };

        //     _context.Movie.Add(movie);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(
        //         "GetSingleMovie",
        //         new { id = movie.Id },
        //         new MovieDTO()
        //         {
        //             ReleaseDate = movie.ReleaseDate,
        //             Rating = movie.Rating,
        //             Title = movie.Title,
        //             Id = movie.Id
        //         }
        //     );
        // }

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
