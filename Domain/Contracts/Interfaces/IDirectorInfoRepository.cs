using Domain.Models.Entities;

namespace Domain.Contracts.Interfaces
{
    public interface IDirectorInfoRepository
    {
        void AddDirector(Director director);

        Task<IEnumerable<Director>> GetDirectorsAsync(string orderBy);

        Task<bool> DirectorExistsAsync(int? Id);

        Task<Director?> GetDirectorAsync(int Id);
    }
}
