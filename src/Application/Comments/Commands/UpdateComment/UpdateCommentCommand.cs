using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Domain.Common;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IUpdateCommentCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommentCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<CommentDto>> Execute(UpdateCommentDto dto)
    {
        var maybe = await _unitOfWork.Comments.Get(dto.Id);
        if (maybe.HasNoValue)
            return Result.Fail<CommentDto>($"There is no comment for the given id:{dto.Id}");

        var comment = _unitOfWork.Comments.Update(maybe.Value, dto);
        await _unitOfWork.CommitAsync();

        return Result.Ok(new CommentDto()
        {
            PostId = comment.PostId,
            Author = comment.Author,
            Content = comment.Content,
            CommentId = comment.Id,
            CreationDate = comment.CreationDate
        });
    }
}