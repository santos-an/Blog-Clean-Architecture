using Application.Interfaces;
using Domain.Entities;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : ICreatePostCommand
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommand(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Execute(CreatePostDto dto)
    {
        var post = new Post()
        {
            Title = dto.Title,
            Content = dto.Content,
            Comments = dto.Comments.Select(c => new Comment()
            {
                Content = c.Content,
                Author = c.Author
            }).ToList()
        };

        await _unitOfWork.Posts.Create(post);
        await _unitOfWork.CommitAsync();
    }
}