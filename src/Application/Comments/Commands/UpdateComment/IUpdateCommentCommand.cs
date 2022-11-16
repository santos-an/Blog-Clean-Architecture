namespace Application.Comments.Commands.UpdateComment;

public interface IUpdateCommentCommand
{
    Task Execute(UpdateCommentDto dto);
}