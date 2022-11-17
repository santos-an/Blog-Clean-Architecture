using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IGetAllCommentsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCommentsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
    public async Task<IEnumerable<CommentListDto>> Execute()
    {
        return await _unitOfWork.Comments.GetAll();
    }
}