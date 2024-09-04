using Infrastructure.Dtos.MovieDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace MovieCardsAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public MoviesController(IServiceManager serviceManager)
        {
            _service = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(
        // [FromQuery] GetMoviesQueryParamDTO paramDTO
        )
        {
            return Ok(await _service.MovieService.GetMoviesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetSingleMovie(int Id)
        {
            return Ok(await _service.MovieService.GetSingleMovieAsync(Id));
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(int Id)
        {
            return Ok(await _service.MovieService.GetMovieDetailsAsync(Id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int Id)
        {
            await _service.MovieService.DeleteMovie(Id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieForCreationDTO movieForCreationDto)
        {
            var outputDto = _service.MovieService.AddMovie(movieForCreationDto);
            await _service.CompleteAsync();

            return CreatedAtAction("GetSingleMovie", new { id = outputDto.Id }, outputDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int Id, MovieForUpdateDTO movieForUpdateDTO)
        {
            await _service.MovieService.UpdateMovie(Id, movieForUpdateDTO);
            await _service.CompleteAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchMovie(
            int Id,
            JsonPatchDocument<MovieForPatchDTO> jsonPatchDocument
        )
        {
            var patchedMovieDto = _service.MovieService.PatchMovie(Id, jsonPatchDocument);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(movieToPatchDto))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(movieToPatchDto, movie);
            await _rm.CompleteAsync();

            return NoContent();
        }
    }
}
