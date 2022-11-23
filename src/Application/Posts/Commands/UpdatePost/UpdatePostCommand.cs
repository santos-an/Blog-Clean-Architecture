using Application.Interfaces;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Domain.Common;

namespace Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IUpdatePostCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostCommand(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PostDto>> Execute(UpdatePostDto dto)
    {
        var maybe = await _unitOfWork.Posts.Get(dto.Id);
        if (maybe.HasNoValue)
            return Result.Fail<PostDto>($"There is no post with the given id:{dto.Id}");
        
        var post = _unitOfWork.Posts.Update(maybe.Value, dto);
        await _unitOfWork.CommitAsync();
        var postDto = _mapper.Map<PostDto>(post);
        
        return Result.Ok(postDto);
    }
}