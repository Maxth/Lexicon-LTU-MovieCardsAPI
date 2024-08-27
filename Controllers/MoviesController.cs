using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCards.Interfaces;
using MovieCardsApi.Data;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCardsDbContext _context;

        public MoviesController(MovieCardsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var dto = _context.Movie.Select(m => new MovieDTO()
            {
                Id = m.Id,
                Rating = m.Rating,
                ReleaseDate = m.ReleaseDate,
                Title = m.Title,
            });

            return Ok(await dto.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetSingleMovie(int Id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(
                new MovieDTO()
                {
                    Id = movie.Id,
                    Rating = movie.Rating,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                }
            );
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int id)
        {
            var dto = await _context
                .Movie.Where(m => m.Id == id)
                .Select(m => new MovieDetailsDTO()
                {
                    Id = m.Id,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    Title = m.Title,
                    Description = m.Description,
                    Genres = m.Genre.Select(g => new GenreDTO(g.Id, g.Name)),
                    Actors = m.Actor.Select(a => new ActorDTO(a.Id, a.Name)),
                    Director = new DirectorDTO(
                        m.Director.Id,
                        m.Director.Name,
                        m.Director.ContactInformation.Email
                    )
                })
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movieToDelete = await _context.Movie.FindAsync(id);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movieToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieForCreationDTO movieForCreationDto)
        {
            if (await TitleAndReleaseDateExist(movieForCreationDto))
            {
                return Conflict("A movie with that title and releasedate already exists");
            }

            Director? director = await FindDirector(movieForCreationDto);

            if (director == null)
            {
                director = new Director()
                {
                    Name = movieForCreationDto.Director.Name,
                    DateOfBirth = movieForCreationDto.Director.DateOfBirth
                };

                _context.Director.Add(director);
                await _context.SaveChangesAsync();
            }

            var movie = new Movie()
            {
                Title = movieForCreationDto.Title,
                ReleaseDate = movieForCreationDto.ReleaseDate,
                Description = movieForCreationDto.Description,
                Rating = movieForCreationDto.Rating,
                DirectorId = director.Id,
            };

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetSingleMovie",
                new { id = movie.Id },
                new MovieDTO()
                {
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Title = movie.Title,
                    Id = movie.Id
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int Id, MovieForUpdateDTO movieForUpdateDTO)
        {
            if (await TitleAndReleaseDateExist(movieForUpdateDTO))
            {
                return Conflict("A movie with that title and releasedate already exists");
            }

            if (
                await _context
                    .Director.Where(d => d.Id == movieForUpdateDTO.DirectorId)
                    .FirstOrDefaultAsync() == null
            )
            {
                return BadRequest("There is no director with that Id");
            }

            var movie = new Movie()
            {
                Id = Id,
                ReleaseDate = movieForUpdateDTO.ReleaseDate,
                Rating = movieForUpdateDTO.Rating,
                Title = movieForUpdateDTO.Title,
                Description = movieForUpdateDTO.Description,
                DirectorId = movieForUpdateDTO.DirectorId,
            };

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TitleAndReleaseDateExist(IMovieCreationOrUpdateDto movieDto)
        {
            return await _context.Movie.AnyAsync(m =>
                String.Equals(m.Title.ToLower(), movieDto.Title.ToLower())
                && m.ReleaseDate == movieDto.ReleaseDate
            );
        }

        private async Task<Director?> FindDirector(MovieForCreationDTO movieDto)
        {
            return await _context.Director.FirstOrDefaultAsync(d =>
                string.Equals(d.Name.ToLower(), movieDto.Director.Name.ToLower())
                && d.DateOfBirth == movieDto.Director.DateOfBirth
            );
        }
    }
}
