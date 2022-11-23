using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using Domain.Common;

namespace Application.Posts.Commands.CreatePost;

public interface ICreatePostCommand
{
    Task<Result<PostDto>> Execute(CreatePostDto dto);
}