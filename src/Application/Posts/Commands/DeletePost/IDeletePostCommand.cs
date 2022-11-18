using Domain.Common;

namespace Application.Posts.Commands.DeletePost;

public interface IDeletePostCommand
{
    Task<Result<bool>> Execute(Guid id);
}