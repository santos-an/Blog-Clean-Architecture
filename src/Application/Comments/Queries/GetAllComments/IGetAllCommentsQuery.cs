namespace Application.Comments.Queries.GetAllComments;

public interface IGetAllCommentsQuery
{
    Task<List<CommentListDto>> Execute();
}