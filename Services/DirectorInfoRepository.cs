using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Data;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Services
{
    public class DirectorInfoRepository : IDirectorInfoRepository
    {
        private readonly MovieCardsDbContext _context;

        public DirectorInfoRepository(MovieCardsDbContext context)
        {
            _context = context;
        }

        public void AddDirector(Director director)
        {
            _context.Director.Add(director);
        }

        public async Task<bool> DirectorExistsAsync(int? Id)
        {
            return await _context.Director.AnyAsync(d => d.Id == Id);
        }

        public async Task<bool> DirectorWithNameAndDobExistsAsync(string name, DateOnly dob)
        {
            return await _context.Director.AnyAsync(d =>
                d.Name.ToLower() == name.ToLower() && d.DateOfBirth == dob
            );
        }

        public async Task<Director?> GetDirectorAsync(int Id)
        {
            return await _context.Director.FirstOrDefaultAsync(d => d.Id == Id);
        }

        public async Task<IEnumerable<Director>> GetDirectorsAsync(string orderBy)
        {
            if (orderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                return await _context.Director.OrderBy(d => d.Name).ToArrayAsync();
            }

            return await _context.Director.OrderBy(d => d.DateOfBirth).ToArrayAsync();
        }
    }
}
