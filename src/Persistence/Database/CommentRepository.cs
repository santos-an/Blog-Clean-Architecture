using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class CommentRepository : ICommentRepository
{
    private readonly BlogContext _context;
    
    public CommentRepository(BlogContext context) => _context = context;

    public async Task<IEnumerable<CommentListDto>> GetAll()
    {
        return await _context.Comments.Select(c => new CommentListDto()
        {
            Id = c.Id,
            PostId = c.PostId,
            Author = c.Author,
            Content = c.Content,
            CreationDate = c.CreationDate
        }).ToListAsync();
    }

    public async Task<CommentDto> Get(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{id} was not found in the database");
        
        return new CommentDto()
        {
            Id = comment.Id,
            PostId = comment.PostId,
            Author = comment.Author,
            Content = comment.Content,
            CreationDate = comment.CreationDate
        };
    }

    public async Task Create(CreateCommentDto dto)
    {
        var post = await  _context.Posts.FirstOrDefaultAsync(p => p.Id == dto.PostId);
        if (post == null)
            throw new EntityNotFoundException($"Post with id:{dto.PostId} not found");
        
        post.Comments.Add(new Comment()
        {
            Author = dto.Author,
            Content = dto.Content,
            CreationDate = DateTime.Now
        });
    }

    public async Task<CommentDto> Update(UpdateCommentDto dto)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == dto.Id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{dto.Id} was not found in the database");
        
        if (!string.IsNullOrEmpty(dto.NewAuthor))
            comment.Author = dto.NewAuthor;
        
        if (!string.IsNullOrEmpty(dto.NewContent))
            comment.Content = dto.NewContent;

        return new CommentDto()
        {
            Id = comment.Id,
            Author = comment.Author,
            Content = comment.Content,
            CreationDate = comment.CreationDate,
            PostId = comment.PostId
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{id} was not found in the database");
        
        _context.Comments.Remove(comment);
        return true;
    }
}