using Application.Interfaces;

namespace Application.Posts.Queries.GetComments;

public class GetCommentsQuery : IGetCommentsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCommentsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PostWithCommentsDto> Execute(Guid id) => await _unitOfWork.Posts.GetComments(id);
}