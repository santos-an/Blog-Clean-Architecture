using Application.Interfaces;
using Domain.Common;

namespace Application.Posts.Commands.DeletePost;

public class DeletePostCommand :IDeletePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Execute(Guid id)
    {
        var post = await _unitOfWork.Posts.Get(id);
        if (post.HasNoValue)
            return Result.Fail<bool>($"There is no post for the given id:{id}");
        
        var completed = await _unitOfWork.Posts.Delete(post.Value);
        await _unitOfWork.CommitAsync();
        
        return Result.Ok(completed);
    }
}