using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using Persistence.Database;

namespace Persistence.Posts;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(30);
        builder.Property(p => p.Content).HasMaxLength(1200);
        
        builder.HasData(DatabaseInitializer.Posts);
    }
}