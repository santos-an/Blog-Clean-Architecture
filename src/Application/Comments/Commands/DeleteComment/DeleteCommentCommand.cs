using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IDeleteCommentCommand
{
    private readonly IDatabaseService _service;

    public DeleteCommentCommand(IDatabaseService service) => _service = service;

    public async Task Execute(Guid id)
    {
        var comment = await _service.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            throw new EntityNotFoundException($"The comment with id:{id} was not found in the database");

        _service.Comments.Remove(comment);
        await _service.CommitAsync();
    }
}