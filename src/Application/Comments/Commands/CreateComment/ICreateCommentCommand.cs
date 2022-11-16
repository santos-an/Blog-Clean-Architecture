namespace Application.Comments.Commands.CreateComment;

public interface ICreateCommentCommand
{
    Task Execute(CreateCommentDto dto);
}