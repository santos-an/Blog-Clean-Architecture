using Application.Comments.Queries.GetAllComments;

namespace Application.Comments.Queries.GetByPostId;

public interface IGetCommentByPostIdQuery
{
    Task<List<CommentListDto>> Execute(Guid id);
}