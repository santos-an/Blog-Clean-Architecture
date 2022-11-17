using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;

namespace Application.Posts.Queries.GetSinglePost;

public class GetSinglePostQuery : IGetSinglePostQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSinglePostQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PostDto> Execute(Guid id) => await _unitOfWork.Posts.Get(id);
}