using Application.Comments.Commands.UpdateComment;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class CommentRepository : ICommentRepository
{
    private readonly BlogContext _context;

    public CommentRepository(BlogContext context) => _context = context;

    public async Task<IEnumerable<Comment>> GetAll() => await _context.Comments.ToListAsync();

    public async Task<Maybe<Comment>> Get(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        return new Maybe<Comment>(comment);
    }

    public void Create(Post post, Comment comment) => post.Comments.Add(comment);

    public Comment Update(Comment comment, UpdateCommentDto dto)
    {
        if (!string.IsNullOrEmpty(dto.NewAuthor))
            comment.Author = dto.NewAuthor;
        
        if (!string.IsNullOrEmpty(dto.NewContent))
            comment.Content = dto.NewContent;
        
        return comment;
    }

    public Task<bool> Delete(Comment comment)
    {
        _context.Comments.Remove(comment);

        return Task.FromResult(Task.CompletedTask.IsCompleted);
    }
}