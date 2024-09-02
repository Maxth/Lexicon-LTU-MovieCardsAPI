using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCardsAPI.Constant;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Configurations
{
    public class ActorConfigurations : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder
                .HasIndex(x => new { x.Name, x.DateOfBirth }, Constants.UniqueActorIndex)
                .IsUnique(true);
        }
    }
}
