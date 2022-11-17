namespace Application.Posts.Queries.GetAllPosts;

public interface IGetAllPostsQuery
{
    Task<IEnumerable<PostListDto>> Execute();
}