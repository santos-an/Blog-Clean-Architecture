using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;

namespace Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IUpdatePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public async Task<PostDto> Execute(UpdatePostDto dto)
    {
        var result = await _unitOfWork.Posts.Update(dto);
        await _unitOfWork.CommitAsync();

        return result;
    }
}