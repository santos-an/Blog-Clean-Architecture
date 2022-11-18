using Domain.Common;

namespace Application.Comments.Commands.DeleteComment;

public interface IDeleteCommentCommand
{
    Task<Result<bool>> Execute(Guid id);
}