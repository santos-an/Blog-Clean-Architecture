using Application.Posts.Commands.UpdatePost;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostRepository
{
    public Task<IEnumerable<Post>> GetAll();
    public Task<Maybe<Post>> Get(Guid id);
    public Task<Maybe<IEnumerable<Comment>>> GetComments(Guid id);
    public Task Create(Post post);
    public Post Update(Post post, UpdatePostDto dto);
    public Task<bool> Delete(Post post);
}