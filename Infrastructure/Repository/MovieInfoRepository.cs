using Domain.Contracts.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovieInfoRepository : IMovieInfoRepository
    {
        private readonly MovieCardsDbContext _context;

        public MovieInfoRepository(MovieCardsDbContext context)
        {
            _context = context;
        }

        public void AddMovie(Movie movie)
        {
            _context.Movie.Add(movie);
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
            return await _context.Movie.ToArrayAsync();
        }

        // public async Task<IEnumerable<Movie>> GetMoviesAsync(GetMoviesQueryParamDTO paramDTO)
        // {
        //     return await GetMoviesAsync();
        // }

        public async Task<Movie?> GetSingleMovieAsync(int Id)
        {
            return await _context.Movie.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public Task<bool> MovieWithTitleAndReleaseDateExistsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
