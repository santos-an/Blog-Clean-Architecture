using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Database;

namespace Persistence.Posts;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(30);
        builder.Property(p => p.Content).HasMaxLength(1200);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Post);
        
        builder.HasData(DatabaseInitializer.Posts);
    }
}