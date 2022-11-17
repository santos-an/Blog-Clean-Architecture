namespace Application.Posts.Commands.DeletePost;

public interface IDeletePostCommand
{
    Task Execute(Guid id);
}