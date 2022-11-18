using Application.Interfaces;
using Application.Posts.Commands.UpdatePost;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class PostRepository : IPostRepository
{
    private readonly BlogContext _context;

    public PostRepository(BlogContext context) => _context = context;

    public async Task<IEnumerable<Post>> GetAll()
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<Maybe<Post>> Get(Guid id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        return new Maybe<Post>(post);
    }

    public async Task<Maybe<IEnumerable<Comment>>> GetComments(Guid id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return new Maybe<IEnumerable<Comment>>();

        return new Maybe<IEnumerable<Comment>>(post.Comments);
    }

    public async Task Create(Post post) => await _context.Posts.AddAsync(post);

    public Post Update(Post post, UpdatePostDto dto)
    {
        if (!string.IsNullOrEmpty(dto.Content))
            post.Content = dto.Content;
        if (!string.IsNullOrEmpty(dto.Title))
            post.Title = dto.Title;

        return post;
    }

    public Task<bool> Delete(Post post)
    {
        _context.Posts.Remove(post);

        return Task.FromResult(Task.CompletedTask.IsCompleted);
    }
}