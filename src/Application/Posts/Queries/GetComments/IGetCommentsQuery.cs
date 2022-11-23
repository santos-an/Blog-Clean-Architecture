using Application.Comments.Queries.GetComment;
using Domain.Common;

namespace Application.Posts.Queries.GetComments;

public interface IGetCommentsQuery
{
    Task<Result<IEnumerable<CommentDto>>> Execute(Guid id);
}