namespace Application.Posts.Commands.CreatePost;

public interface ICreatePostCommand
{
    Task Execute(CreatePostDto dto);
}