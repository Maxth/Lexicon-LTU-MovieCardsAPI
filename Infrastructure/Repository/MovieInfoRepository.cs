using Domain.Contracts.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovieInfoRepository : RepositoryBase<Movie>, IMovieInfoRepository
    {
        public MovieInfoRepository(MovieCardsDbContext context)
            : base(context) { }

        public async Task AddMovieAsync(Movie movie) => await CreateAsync(movie);

        public async Task<int> DeleteMovieAsync(int Id) =>
            await GetByCondition(m => m.Id == Id).ExecuteDeleteAsync();

        public async Task<bool> Exists(int Id) => await GetByCondition(m => m.Id == Id).AnyAsync();

        public async Task<Movie?> GetMovieDetailsAsync(int Id, bool trackChanges = false) =>
            await GetByCondition(m => m.Id == Id, trackChanges)
                .Include(m => m.Genre)
                .Include(m => m.Actor)
                .Include(m => m.Director)
                .ThenInclude(d => d.ContactInformation)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Movie>> GetMoviesAsync(bool trackChanges = false) =>
            await GetAll(trackChanges).ToListAsync();

        public async Task<Movie?> GetSingleMovieAsync(int Id, bool trackChanges = false) =>
            await GetByCondition(m => m.Id == Id, trackChanges).FirstOrDefaultAsync();
    }
}
