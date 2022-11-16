using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetSingleComment;

public class GetSingleSingleCommentQuery : IGetSingleCommentQuery
{
    private readonly IDatabaseService _service;
    
    public GetSingleSingleCommentQuery(IDatabaseService service) => _service = service;

    public async Task<CommentDto> Execute(Guid id)
    {
        var comment = await _service.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{id} was not found in the database");

        return new CommentDto()
        {
            Id = comment.Id,
            PostId = comment.PostId,
            Author = comment.Author,
            Content = comment.Author,
            CreationDate = comment.CreationDate
        };
    }
}