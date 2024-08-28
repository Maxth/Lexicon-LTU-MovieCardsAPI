using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Configurations
{
    public class DirectorConfigurations : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasIndex(x => new { x.Name, x.DateOfBirth }).IsUnique(true);
        }
    }
}
