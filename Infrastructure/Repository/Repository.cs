using Domain.Contracts.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly MovieCardsDbContext _context;

        public Repository(MovieCardsDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
