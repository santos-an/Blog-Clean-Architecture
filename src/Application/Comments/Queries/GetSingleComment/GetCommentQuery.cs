using Application.Interfaces;
using Domain.Common;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetSingleComment;

public class GetCommentQuery : IGetCommentQuery
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetCommentQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<CommentDto>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Comments.Get(id);
        if (maybe.HasNoValue)
            return Result.Fail<CommentDto>($"There is no comment for the given id:{id}");

        var comment = maybe.Value;
        var dto = new CommentDto()
        {
            PostId = comment.PostId,
            CommentId = comment.Id,
            Author = comment.Author,
            Content = comment.Content,
            CreationDate = comment.CreationDate
        };
        
        return Result.Ok(dto);
    }
}