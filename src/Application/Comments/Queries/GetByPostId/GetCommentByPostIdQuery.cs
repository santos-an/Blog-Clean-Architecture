using Application.Comments.Queries.GetAllComments;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetByPostId;

public class GetCommentByPostIdQuery : IGetCommentByPostIdQuery
{
    private readonly IUnitOfWork _service;

    public GetCommentByPostIdQuery(IUnitOfWork service) => _service = service;

    public async Task<List<CommentListDto>> Execute(Guid id)
    {
        return null;
        
        // var post = await _service.Posts
        //     .Include(p => p.Comments)
        //     .FirstOrDefaultAsync(p => p.Id == id);
        // if (post == null)
        //     throw new EntityNotFoundException($"There is no post with id:{id} found on the database");
        //
        // return post.Comments.Select(c => new CommentListDto()
        // {
        //     Id = c.Id,
        //     Author = c.Author,
        //     Content = c.Content,
        //     PostId = c.PostId,
        //     CreationDate = c.CreationDate
        // }).ToList();
    }
}