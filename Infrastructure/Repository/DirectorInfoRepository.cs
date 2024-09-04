using Domain.Contracts.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DirectorInfoRepository : RepositoryBase<Director>, IDirectorInfoRepository
    {
        public DirectorInfoRepository(MovieCardsDbContext context)
            : base(context) { }

        public async Task<Director?> GetDirectorAsync(int Id, bool trackChanges = false) =>
            await GetByCondition(d => d.Id == Id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Director>> GetDirectorsAsync(bool trackChanges = false) =>
            await GetAll(trackChanges).ToListAsync();
    }
}
