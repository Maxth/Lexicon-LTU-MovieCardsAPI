using Domain.Models.Entities;

namespace Domain.Contracts.Interfaces
{
    public interface IDirectorInfoRepository : IRepositoryCommon<Director>
    {
        Task<IEnumerable<Director>> GetDirectorsAsync(bool trackChanges = false);

        Task<Director?> GetDirectorAsync(int Id, bool trackChanges = false);
    }
}
