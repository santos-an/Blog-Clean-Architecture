using Domain.Entities;

namespace Persistence.Database;

public class DatabaseInitializer
{
    public static readonly IEnumerable<Post> Posts = new List<Post>()
    {
        new() { Id = Guid.NewGuid(), Title = "Post 1", Content = "Content 1", CreationDate = DateTime.Now, },
        new() { Id = Guid.NewGuid(), Title = "Post 2", Content = "Content 2", CreationDate = DateTime.Now, },
        new() { Id = Guid.NewGuid(), Title = "Post 3", Content = "Content 3", CreationDate = DateTime.Now, },
        new() { Id = Guid.NewGuid(), Title = "Post 4", Content = "Content 4", CreationDate = DateTime.Now, },
        new() { Id = Guid.NewGuid(), Title = "Post 5", Content = "Content 5", CreationDate = DateTime.Now, }
    };

    public static readonly IEnumerable<Comment> Comments = new List<Comment>()
    {
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(0).Id, Author = "Author 1", Content = "Comment 1 about post 1",
            CreationDate = DateTime.Now
        },
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(0).Id, Author = "Author 2", Content = "Comment 2 about post 1",
            CreationDate = DateTime.Now
        },
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(1).Id, Author = "Author 3", Content = "Comment 1 about post 2",
            CreationDate = DateTime.Now
        },
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(1).Id, Author = "Author 4", Content = "Comment 2 about post 2",
            CreationDate = DateTime.Now
        },
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(2).Id, Author = "Author 5", Content = "Comment 1 about post 3",
            CreationDate = DateTime.Now
        },
        new()
        {
            Id = Guid.NewGuid(), PostId = Posts.ElementAt(2).Id, Author = "Author 6", Content = "Comment 2 about post 3",
            CreationDate = DateTime.Now
        },
    };
}