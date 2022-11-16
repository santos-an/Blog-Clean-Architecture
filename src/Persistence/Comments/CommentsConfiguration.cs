using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Database;

namespace Persistence.Comments;

public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Content).HasMaxLength(120);
        builder.Property(c => c.Author).HasMaxLength(30);
        builder.HasOne(c => c.Post).WithMany(p => p.Comments);

        builder.HasData(DatabaseInitializer.Comments);
    }
}