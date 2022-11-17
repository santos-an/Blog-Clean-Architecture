namespace Application.Comments.Queries.GetAllComments;

public interface IGetAllCommentsQuery
{
    Task<IEnumerable<CommentListDto>> Execute();
}