using System.Net;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : ICreateCommentCommand
{
    private readonly IDatabaseService _service;

    public CreateCommentCommand(IDatabaseService service) => _service = service;

    public async Task Execute(CreateCommentDto dto)
    {
        var post = await _service.Posts.FirstOrDefaultAsync(p => p.Id == dto.PostId);
        if (post == null)
            throw new EntityNotFoundException($"Post with id:{dto.PostId} not found");
        
        post.Comments.Add(new Comment()
        {
            Author = dto.Author,
            Content = dto.Content,
            CreationDate = DateTime.Now
        });

        await _service.CommitAsync();
    }
}