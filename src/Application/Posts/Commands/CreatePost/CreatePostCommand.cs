using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Domain.Common;
using Domain.Entities;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : ICreatePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<PostDto>> Execute(CreatePostDto dto)
    {
        var post = new Post()
        {
            Title = dto.Title,
            Content = dto.Content,
            Comments = dto.Comments.Select(c => new Comment()
            {
                Content = c.Content,
                Author = c.Author,
            }).ToList()
        };

        await _unitOfWork.Posts.Create(post);
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