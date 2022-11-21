using Application.Posts.Queries.GetPost;

namespace Application.Posts.Queries.GetAllPosts;

public interface IGetAllPostsQuery
{
    Task<IEnumerable<PostDto>> Execute();
}