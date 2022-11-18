using Application.Comments.Queries.GetComment;
using Domain.Common;

namespace Application.Comments.Commands.UpdateComment;

public interface IUpdateCommentCommand
{
    Task<Result<CommentDto>> Execute(UpdateCommentDto dto);
}