using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using MovieCardsAPI.Configurations;
using MovieCardsApi.Entities;

namespace MovieCardsApi.Data
{
    public class MovieCardsDbContext : DbContext
    {
        public MovieCardsDbContext(DbContextOptions<MovieCardsDbContext> options)
            : base(options) { }

        public DbSet<Director> Director => Set<Director>();
        public DbSet<Movie> Movie => Set<Movie>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DirectorConfigurations());
            modelBuilder.ApplyConfiguration(new MovieConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }
    }
}
