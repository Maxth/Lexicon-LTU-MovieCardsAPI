using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCardsAPI.Constant;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Configurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasIndex(x => new { x.Title, x.ReleaseDate }, Constants.UniqueMovieIndex)
                .IsUnique(true);
        }
    }
}
