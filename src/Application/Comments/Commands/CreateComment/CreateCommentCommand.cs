using Application.Interfaces;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : ICreateCommentCommand
{
    private readonly IUnitOfWork _service;

    public CreateCommentCommand(IUnitOfWork service) => _service = service;

    public async Task Execute(CreateCommentDto dto)
    {
        await _service.Comments.Create(dto);
        await _service.CommitAsync();
    }
}