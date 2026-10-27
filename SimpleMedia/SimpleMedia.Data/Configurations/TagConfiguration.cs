using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMedia.Data.Models;

namespace SimpleMedia.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public static readonly List<Tag> Tags = new List<Tag>
        {
            new Tag { Id = 1, Name = "General" },
            new Tag { Id = 2, Name = "Discussion" },
            new Tag { Id = 3, Name = "Gaming" },
            new Tag { Id = 4, Name = "Music" },
            new Tag { Id = 5, Name = "Movies" },
            new Tag { Id = 6, Name = "Technology" },
            new Tag { Id = 7, Name = "Travel" },
            new Tag { Id = 8, Name = "Lifestyle" }
        };

        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasIndex(t => t.Name)
                .IsUnique();

            builder.HasData(Tags);
        }
    }
}
