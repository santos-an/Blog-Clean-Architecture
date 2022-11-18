using Application.Posts.Queries.GetAllPosts;
using Domain.Common;

namespace Application.Posts.Queries.GetSinglePost;

public interface IGetPostQuery
{
    Task<Result<PostDto>> Execute(Guid id);
}