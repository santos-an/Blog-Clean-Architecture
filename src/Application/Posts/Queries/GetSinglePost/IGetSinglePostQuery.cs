using Application.Posts.Queries.GetAllPosts;

namespace Application.Posts.Queries.GetSinglePost;

public interface IGetSinglePostQuery
{
    Task<PostDto> Execute(Guid id);
}