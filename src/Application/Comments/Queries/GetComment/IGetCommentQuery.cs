using Domain.Common;

namespace Application.Comments.Queries.GetComment;

public interface IGetCommentQuery
{
    Task<Result<CommentDto>> Execute(Guid id);
}