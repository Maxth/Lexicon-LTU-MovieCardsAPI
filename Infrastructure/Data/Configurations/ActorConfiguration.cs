using Domain.Constants;
using Domain.Models.Entities;
using Domain.Models.Entities.Joins;
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
            builder
                .HasMany(a => a.Movie)
                .WithMany(m => m.Actor)
                .UsingEntity<ActorMovie>(
                    r =>
                        r.HasOne(e => e.Movie)
                            .WithMany(e => e.ActorMovie)
                            .HasForeignKey(e => e.MovieId)
                            .HasConstraintName(ConstVars.FK_ActorMovie_Movie),
                    l =>
                        l.HasOne(e => e.Actor)
                            .WithMany(e => e.ActorMovie)
                            .HasForeignKey(e => e.ActorId)
                            .HasConstraintName(ConstVars.FK_ActorMovie_Actor)
                );
        }
    }
}
