using Application.Comments.Queries.GetComment;

namespace Application.Comments.Queries.GetAllComments;

public interface IGetAllCommentsQuery
{
    Task<IEnumerable<CommentDto>> Execute();
}