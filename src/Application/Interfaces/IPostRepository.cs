using Application.Posts.Queries.GetAllPosts;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPostRepository
{
    public Task<IEnumerable<PostListDto>> GetAll();
    public Post Get(Guid id);
    public Post Create(Post post);
    public Post Update(Post post);
    public bool Delete(Guid id);
}