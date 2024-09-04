using Domain.Models.Entities;
using dotenv.net;
using EntityFramework.Exceptions.PostgreSQL;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MovieCardsDbContext : DbContext
    {
        public MovieCardsDbContext(DbContextOptions<MovieCardsDbContext> options)
            : base(options) { }

        public DbSet<Director> Director => Set<Director>();
        public DbSet<Movie> Movie => Set<Movie>();
        public DbSet<Actor> Actor => Set<Actor>();

        private readonly string _pathToEnv =
            $"{Directory.GetCurrentDirectory()}/../Infrastructure/.env";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DirectorConfigurations());
            modelBuilder.ApplyConfiguration(new MovieConfigurations());
            modelBuilder.ApplyConfiguration(new ActorConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { _pathToEnv }));
            string? dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            optionsBuilder
                .UseNpgsql(
                    dbUrl
                        ?? throw new InvalidOperationException("Connection string resolved to null")
                )
                .UseExceptionProcessor();
        }
    }
}
