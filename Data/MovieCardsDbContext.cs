using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Entities;

namespace MovieCardsApi.Data
{
    public class MovieCardsDbContext : DbContext
    {
        public MovieCardsDbContext(DbContextOptions<MovieCardsDbContext> options)
            : base(options) { }

        public DbSet<Director> Director => Set<Director>();
        public DbSet<Movie> Movie => Set<Movie>();
    }
}
