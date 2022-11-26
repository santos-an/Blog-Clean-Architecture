using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : ICreateCommentCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommand(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CommentDto>> Execute(CreateCommentDto dto)
    {
        var maybe = await _unitOfWork.Posts.Get(dto.PostId);
        if (maybe.HasNoValue)
            return Result.Fail<CommentDto>($"There is no post for the give id:{dto.PostId}");

        var post = maybe.Value;
        var comment = new Comment() { Author = dto.Author, Content = dto.Content, Post = post };
        
        await _unitOfWork.Comments.Create(comment);
        await _unitOfWork.CommitAsync();
        
        var commentDto = _mapper.Map<CommentDto>(comment);
        return Result.Ok(commentDto);
    }
}