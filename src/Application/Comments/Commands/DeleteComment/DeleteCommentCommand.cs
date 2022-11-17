using Application.Interfaces;

namespace Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IDeleteCommentCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(Guid id)
    {
        await _unitOfWork.Comments.Delete(id);
        await _unitOfWork.CommitAsync();
    }
}