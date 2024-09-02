using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCardsAPI.Constant;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Configurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasIndex(x => x.Title, Constants.UniqueMovieIndex).IsUnique(true);
            //Ensure releasedate can only be set on first insert
            builder
                .Property(x => x.ReleaseDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        }
    }
}
