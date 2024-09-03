using Domain.Constants;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ActorConfigurations : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder
                .HasIndex(x => new { x.Name, x.DateOfBirth }, ConstVars.UniqueActorIndex)
                .IsUnique(true);
        }
    }
}
