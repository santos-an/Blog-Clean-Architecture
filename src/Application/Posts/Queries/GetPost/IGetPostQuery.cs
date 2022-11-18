using Application.Posts.Queries.GetAllPosts;
using Domain.Common;

namespace Application.Posts.Queries.GetPost;

public interface IGetPostQuery
{
    Task<Result<PostDto>> Execute(Guid id);
}