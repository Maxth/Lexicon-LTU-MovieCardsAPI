using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Models;

namespace MovieCardsApi.Data
{
    public class MovieCardsDbContext : DbContext
    {
        public MovieCardsDbContext(DbContextOptions<MovieCardsDbContext> options)
            : base(options) { }

        public DbSet<Director> Director => Set<Director>();
    }
}
