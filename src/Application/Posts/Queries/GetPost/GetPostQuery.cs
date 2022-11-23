using Application.Interfaces;
using AutoMapper;
using Domain.Common;

namespace Application.Posts.Queries.GetPost;

public class GetPostQuery : IGetPostQuery
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPostQuery(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PostDto>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Posts.Get(id);
        if (maybe.HasNoValue)
            return Result.Fail<PostDto>($"There is no post with the id:{id}");

        var post = maybe.Value;
        var dto = _mapper.Map<PostDto>(post);
        
        return Result.Ok(dto);
    }
}