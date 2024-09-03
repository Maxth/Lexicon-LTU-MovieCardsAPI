using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data.ContextFactory;

public class ContextFactory : IDesignTimeDbContextFactory<MovieCardsDbContext>
{
    public MovieCardsDbContext CreateDbContext(string[] args)
    {
        DotEnv.Load();
        string? dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        var optionsBuilder = new DbContextOptionsBuilder<MovieCardsDbContext>();
        optionsBuilder.UseNpgsql(
            dbUrl ?? throw new InvalidOperationException("Connection string resolved to null")
        );

        return new MovieCardsDbContext(optionsBuilder.Options);
    }
}
