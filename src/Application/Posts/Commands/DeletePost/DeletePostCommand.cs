using Application.Interfaces;

namespace Application.Posts.Commands.DeletePost;

public class DeletePostCommand :IDeletePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(Guid id)
    {
        await _unitOfWork.Posts.Delete(id);
        await _unitOfWork.CommitAsync();
    }
}