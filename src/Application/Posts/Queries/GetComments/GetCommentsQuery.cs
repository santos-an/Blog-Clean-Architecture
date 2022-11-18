using Application.Interfaces;
using Domain.Common;

namespace Application.Posts.Queries.GetComments;

public class GetCommentsQuery : IGetCommentsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCommentsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<CommentDto>>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Posts.GetComments(id);
        if (maybe.HasNoValue)
            return Result.Fail<IEnumerable<CommentDto>>($"There is no post for the given id:{id}");

        var comments = maybe.Value;
        return Result.Ok<IEnumerable<CommentDto>>(comments.Select(c => new CommentDto()
        {
            Id = c.Id,
            Author = c.Author,
            Content = c.Content,
            CreationDate = c.CreationDate
        }).ToList());
    }
}