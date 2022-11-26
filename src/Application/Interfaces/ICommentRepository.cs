using Application.Comments.Commands.UpdateComment;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<Comment>> GetAll();
    public Task<Maybe<Comment>> Get(Guid id);
    public Task Create(Comment comment);
    public Comment Update(Comment comment, UpdateCommentDto dto);
    public Task<bool> Delete(Comment comment);
}