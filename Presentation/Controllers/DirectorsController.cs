using Domain.Models.Entities;
using Infrastructure.Dtos.DirectorDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace MovieCardsAPI.Controllers
{
    [Route("api/directors")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DirectorsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors()
        {
            var directors = await _service.DirectorService.GetDirectorsAsync();
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDTO>> GetSingleDirector(int Id)
        {
            var director = await _service.DirectorService.GetDirectorAsync(Id);
            return Ok(director);
        }

        [HttpPost]
        public async Task<ActionResult> AddDirector(DirectorForCreationDTO inputDto)
        {
            var outputDto = await _service.DirectorService.AddDirector(inputDto);
            await _service.CompleteAsync();

            return CreatedAtAction("GetSingleDirector", new { id = outputDto.Id }, outputDto);
        }
    }
}
