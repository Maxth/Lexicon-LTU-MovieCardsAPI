using MovieCardsApi.Entities;

namespace MovieCardsAPI.Services
{
    public interface IDirectorInfoRepository
    {
        Task<bool> DirectorWithNameAndDobExistsAsync(string name, DateOnly dob);

        void AddDirector(Director director);

        Task<IEnumerable<Director>> GetDirectorsAsync(string orderBy);

        Task<bool> DirectorExistsAsync(int? Id);

        Task<Director?> GetDirectorAsync(int Id);
    }
}
