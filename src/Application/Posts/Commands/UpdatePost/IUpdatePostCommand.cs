using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using Domain.Common;

namespace Application.Posts.Commands.UpdatePost;

public interface IUpdatePostCommand
{
    Task<Result<PostDto>> Execute(UpdatePostDto dto);
}