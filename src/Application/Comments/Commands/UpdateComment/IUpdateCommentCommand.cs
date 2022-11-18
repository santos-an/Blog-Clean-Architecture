using Application.Comments.Queries.GetSingleComment;
using Domain.Common;

namespace Application.Comments.Commands.UpdateComment;

public interface IUpdateCommentCommand
{
    Task<Result<CommentDto>> Execute(UpdateCommentDto dto);
}