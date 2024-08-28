using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Data;
using MovieCardsApi.Entities;
using Npgsql.Internal;

namespace MovieCardsAPI.Services
{
    public class MovieInfoRepository : IMovieInfoRepository
    {
        private readonly MovieCardsDbContext _context;

        public MovieInfoRepository(MovieCardsDbContext context)
        {
            _context = context;
        }

        public void DeleteMovie(Movie movie)
        {
            _context.Movie.Remove(movie);
        }

        public async Task<Movie?> GetMovieDetailsAsync(int Id)
        {
            return await _context
                .Movie.Where(m => m.Id == Id)
                .Include(m => m.Genre)
                .Include(m => m.Actor)
                .Include(m => m.Director)
                .ThenInclude(d => d.ContactInformation)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _context.Movie.OrderBy(m => m.ReleaseDate).ToArrayAsync();
        }

        public async Task<Movie?> GetSingleMovieAsync(int Id)
        {
            return await _context.Movie.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<bool> MovieExistsAsync(int Id)
        {
            return await _context.Movie.AnyAsync(m => m.Id == Id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
