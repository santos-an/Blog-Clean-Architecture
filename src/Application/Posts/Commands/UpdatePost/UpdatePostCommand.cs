﻿using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Domain.Common;

namespace Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IUpdatePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<PostDto>> Execute(UpdatePostDto dto)
    {
        var maybe = await _unitOfWork.Posts.Get(dto.Id);
        if (maybe.HasNoValue)
            return Result.Fail<PostDto>($"There is no post with the given id:{dto.Id}");
        
        var post = _unitOfWork.Posts.Update(maybe.Value, dto);
        await _unitOfWork.CommitAsync();

        return Result.Ok(new PostDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreationDate = post.CreationDate,
            Comments = post.Comments.Select(c => new CommentDto()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                CreationDate = c.CreationDate
            }).ToList()
        });
    }
}