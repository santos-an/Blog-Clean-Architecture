using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class PostRepository : IPostRepository
{
    private readonly BlogContext _context;

    public PostRepository(BlogContext context) => _context = context;

    public async Task<IEnumerable<PostListDto>> GetAll()
    {
        var posts = _context.Posts
            .Include(p => p.Comments)
            .Select(p => new PostListDto()
            {
                Id = p.Id,
                Content = p.Content,
                Title = p.Title,
                CreationDate = p.CreationDate,
                Comments = p.Comments.Select(c => new CommentDto()
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    CreationDate = c.CreationDate
                }).ToList()
            });

        return await posts.ToListAsync();
    }

    public Post Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Post Create(Post post)
    {
        throw new NotImplementedException();
    }

    public Post Update(Post post)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}