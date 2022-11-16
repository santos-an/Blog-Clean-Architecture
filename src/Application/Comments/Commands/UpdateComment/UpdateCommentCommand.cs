using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IUpdateCommentCommand
{
    private readonly IDatabaseService _service;

    public UpdateCommentCommand(IDatabaseService service) => _service = service;

    public async Task Execute(UpdateCommentDto dto)
    {
        var comment = await _service.Comments.FirstOrDefaultAsync(c => c.Id == dto.Id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{dto.Id} was not found in the database");

        if (!string.IsNullOrEmpty(dto.NewAuthor))
            comment.Author = dto.NewAuthor;
        
        if (!string.IsNullOrEmpty(dto.NewContent))
            comment.Content = dto.NewContent;

        await _service.CommitAsync();
    }
}