using Application.Interfaces;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IUpdateCommentCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommentCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(UpdateCommentDto dto)
    {
        await _unitOfWork.Comments.Update(dto);
        await _unitOfWork.CommitAsync();
    }
}