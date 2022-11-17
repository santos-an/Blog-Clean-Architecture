using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;

namespace Application.Interfaces;

public interface IPostRepository
{
    public Task<IEnumerable<PostDto>> GetAll();
    public Task<PostDto> Get(Guid id);
    public Task<PostWithCommentsDto> GetComments(Guid id);
    public Task Create(CreatePostDto post);
    public Task<PostDto> Update(UpdatePostDto post);
    public Task<bool> Delete(Guid id);
}