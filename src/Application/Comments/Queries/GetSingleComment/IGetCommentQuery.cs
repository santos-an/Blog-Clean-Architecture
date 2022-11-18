using Domain.Common;

namespace Application.Comments.Queries.GetSingleComment;

public interface IGetCommentQuery
{
    Task<Result<CommentDto>> Execute(Guid id);
}