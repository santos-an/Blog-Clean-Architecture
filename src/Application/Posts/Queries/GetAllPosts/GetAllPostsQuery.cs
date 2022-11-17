using Application.Interfaces;

namespace Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQuery : IGetAllPostsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PostDto>> Execute() => await _unitOfWork.Posts.GetAll();
}