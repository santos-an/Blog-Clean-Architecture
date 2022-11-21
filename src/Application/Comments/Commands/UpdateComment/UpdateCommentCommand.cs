using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IUpdateCommentCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommentCommand(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommentDto>> Execute(UpdateCommentDto dto)
    {
        var maybe = await _unitOfWork.Comments.Get(dto.Id);
        if (maybe.HasNoValue)
            return Result.Fail<CommentDto>($"There is no comment for the given id:{dto.Id}");

        var comment = _unitOfWork.Comments.Update(maybe.Value, dto);
        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<CommentDto>(comment);
        return Result.Ok(result);
    }
}