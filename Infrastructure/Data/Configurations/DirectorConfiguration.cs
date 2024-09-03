using Domain.Constants;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DirectorConfigurations : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder
                .HasIndex(x => new { x.Name, x.DateOfBirth }, ConstVars.UniqueDirectorIndex)
                .IsUnique(true);
        }
    }
}
