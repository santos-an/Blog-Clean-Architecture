using Application.Comments.Queries.GetComment;
using Domain.Common;

namespace Application.Comments.Commands.CreateComment;

public interface ICreateCommentCommand
{
    Task<Result<CommentDto>> Execute(CreateCommentDto dto);
}