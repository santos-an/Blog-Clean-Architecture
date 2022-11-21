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
        var post = await _unitOfWork.Posts.Get(dto.PostId);
        if (post.HasNoValue)
            return Result.Fail<CommentDto>($"There is no post for the give id:{dto.PostId}");

        var comment = new Comment() { Author = dto.Author, Content = dto.Content };
        _unitOfWork.Comments.Create(post.Value, comment);
        await _unitOfWork.CommitAsync();
        
        var result = _mapper.Map<CommentDto>(comment);
        return Result.Ok(result);
    }
}