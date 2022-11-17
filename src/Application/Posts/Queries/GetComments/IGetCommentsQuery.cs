namespace Application.Posts.Queries.GetComments;

public interface IGetCommentsQuery
{
    Task<PostWithCommentsDto> Execute(Guid id);
}