namespace Application.Posts.Queries;

public interface IGetPostsListQuery
{
    Task<List<PostsListDto>> Execute();
}