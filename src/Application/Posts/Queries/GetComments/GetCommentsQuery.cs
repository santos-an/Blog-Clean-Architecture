using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;

namespace Application.Posts.Queries.GetComments;

public class GetCommentsQuery : IGetCommentsQuery
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCommentsQuery(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<CommentDto>>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Posts.GetComments(id);
        if (maybe.HasNoValue)
            return Result.Fail<IEnumerable<CommentDto>>($"There is no post for the given id:{id}");

        var comments = maybe.Value;
        var commentsDto = comments
            .Select(
                comment => _mapper.Map<CommentDto>(comment))
            .ToList();
        
        return Result.Ok<IEnumerable<CommentDto>>(commentsDto);
    }
}