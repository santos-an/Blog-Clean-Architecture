using Domain;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICommentRepository
{
    public IEnumerable<Comment> GetAll();
    public Comment Get(Guid id);
    public IEnumerable<Comment> GetByPostId(Guid postId);
    public Comment Create(Comment comment);
    public Comment Update(Comment comment);
    public bool Delete(Guid id);
}