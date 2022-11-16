namespace Application.Posts.Queries.GetAllPosts;

public interface IGetAllPostsQuery
{
    Task<List<PostListDto>> Execute();
}