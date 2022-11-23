using Application.Interfaces;
using AutoMapper;
using Domain.Common;

namespace Application.Comments.Queries.GetComment;

public class GetCommentQuery : IGetCommentQuery
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCommentQuery(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommentDto>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Comments.Get(id);
        if (maybe.HasNoValue)
            return Result.Fail<CommentDto>($"There is no comment for the given id:{id}");

        var comment = maybe.Value;
        var result = _mapper.Map<CommentDto>(comment);
        
        return Result.Ok(result);
    }
}