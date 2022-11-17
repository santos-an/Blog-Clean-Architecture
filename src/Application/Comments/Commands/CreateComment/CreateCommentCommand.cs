using Application.Interfaces;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : ICreateCommentCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(CreateCommentDto dto)
    {
        await _unitOfWork.Comments.Create(dto);
        await _unitOfWork.CommitAsync();
    }
}