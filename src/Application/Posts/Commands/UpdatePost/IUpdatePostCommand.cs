using Application.Posts.Queries.GetAllPosts;

namespace Application.Posts.Commands.UpdatePost;

public interface IUpdatePostCommand
{
    Task<PostDto> Execute(UpdatePostDto dto);
}