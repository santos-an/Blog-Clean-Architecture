using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;

namespace Application.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IGetAllCommentsQuery
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCommentsQuery(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CommentDto>> Execute()
    {
        var comments = await _unitOfWork.Comments.GetAll();
        
        return comments.Select(comment => _mapper.Map<CommentDto>(comment)).ToList();
    }
}