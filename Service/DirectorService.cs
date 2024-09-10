using AutoMapper;
using Domain.Contracts.Interfaces;
using Domain.Exceptions.NotFound;
using Domain.Models.Dtos.DirectorDtos;
using Domain.Models.Entities;
using Service.Contracts;

namespace Service;

public class DirectorService : IDirectorService
{
    private readonly IRepositoryManager _rm;
    private readonly IMapper _mapper;

    public DirectorService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _rm = repositoryManager;
        _mapper = mapper;
    }

    public async Task<DirectorDTO> AddDirector(DirectorForCreationDTO inputDto)
    {
        var director = _mapper.Map<Director>(inputDto);
        await _rm.DirectorInfoRepository.CreateAsync(director);
        await _rm.CompleteAsync();
        return _mapper.Map<DirectorDTO>(director);
    }

    public async Task<DirectorDTO> GetDirectorAsync(int Id, bool trackChanges = false)
    {
        var director = await _rm.DirectorInfoRepository.GetDirectorAsync(Id);

        if (director is null)
        {
            throw new DirectorNotFoundException(Id);
        }

        return _mapper.Map<DirectorDTO>(director);
    }

    public async Task<IEnumerable<DirectorDTO>> GetDirectorsAsync(bool trackChanges = false)
    {
        var directors = await _rm.DirectorInfoRepository.GetDirectorsAsync();

        return _mapper.Map<IEnumerable<DirectorDTO>>(directors);
    }
}
