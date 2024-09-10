using Domain.Models.Dtos.DirectorDtos;

namespace Service.Contracts;

public interface IDirectorService
{
    Task<IEnumerable<DirectorDTO>> GetDirectorsAsync(bool trackChanges = false);

    Task<DirectorDTO> GetDirectorAsync(int Id, bool trackChanges = false);

    Task<DirectorDTO> AddDirector(DirectorForCreationDTO inputDto);
}
