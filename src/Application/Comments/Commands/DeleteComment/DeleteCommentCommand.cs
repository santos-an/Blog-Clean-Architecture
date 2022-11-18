using Application.Interfaces;
using Domain.Common;

namespace Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IDeleteCommentCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Comments.Get(id);
        if (maybe.HasNoValue)
            return Result.Fail<bool>($"There is no comment for the given id:{id}");
        
        var completed = await _unitOfWork.Comments.Delete(maybe.Value);
        await _unitOfWork.CommitAsync();
        
        return Result.Ok(completed);
    }
}