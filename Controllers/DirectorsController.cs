using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using MovieCardsAPI.Constant;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;
using MovieCardsAPI.Services;

namespace MovieCardsAPI.Controllers
{
    [Route("api/directors")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IDirectorInfoRepository _directorInfoRepository;
        private readonly IMapper _mapper;

        public DirectorsController(
            IRepository repository,
            IDirectorInfoRepository directorInfoRepository,
            IMapper mapper
        )
        {
            _repository = repository;
            _directorInfoRepository = directorInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors(
            string orderBy = "dateOfBirth"
        )
        {
            var directors = await _directorInfoRepository.GetDirectorsAsync(orderBy);
            return Ok(_mapper.Map<IEnumerable<DirectorDTO>>(directors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDTO>> GetSingleDirector(int Id)
        {
            var director = await _directorInfoRepository.GetDirectorAsync(Id);

            if (director is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DirectorDTO>(director));
        }

        [HttpPost]
        public async Task<ActionResult> AddDirector(DirectorForCreationDTO dto)
        {
            var director = _mapper.Map<Director>(dto);
            _directorInfoRepository.AddDirector(director);

            await _repository.SaveChangesAsync();

            return CreatedAtAction(
                "GetSingleDirector",
                new { id = director.Id },
                _mapper.Map<DirectorDTO>(director)
            );
        }
    }
}
