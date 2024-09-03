using Domain.Contracts.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
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
