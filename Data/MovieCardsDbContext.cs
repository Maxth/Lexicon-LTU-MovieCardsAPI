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
        public DbSet<Actor> Actor => Set<Actor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DirectorConfigurations());
            modelBuilder.ApplyConfiguration(new MovieConfigurations());
            modelBuilder.ApplyConfiguration(new ActorConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Catch common DB-exceptions in an easy way with the help of package EntityFramework.Exceptions
            optionsBuilder.UseExceptionProcessor();
        }
    }
}
