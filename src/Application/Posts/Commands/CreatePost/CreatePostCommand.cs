using Application.Interfaces;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : ICreatePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(CreatePostDto dto)
    {
        await _unitOfWork.Posts.Create(dto);
        await _unitOfWork.CommitAsync();
    }
}