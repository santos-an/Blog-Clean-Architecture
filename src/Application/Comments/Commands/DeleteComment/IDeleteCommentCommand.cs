namespace Application.Comments.Commands.DeleteComment;

public interface IDeleteCommentCommand
{
    Task Execute(Guid id);
}