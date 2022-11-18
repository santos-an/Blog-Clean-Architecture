using Application.Comments.Queries.GetSingleComment;

namespace Application.Comments.Queries.GetAllComments;

public interface IGetAllCommentsQuery
{
    Task<IEnumerable<CommentDto>> Execute();
}