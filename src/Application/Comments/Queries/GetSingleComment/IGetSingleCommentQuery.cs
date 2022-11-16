namespace Application.Comments.Queries.GetSingleComment;

public interface IGetSingleCommentQuery
{
    Task<CommentDto> Execute(Guid id);
}