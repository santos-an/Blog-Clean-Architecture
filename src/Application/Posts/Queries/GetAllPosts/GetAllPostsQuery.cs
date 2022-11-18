using Application.Interfaces;
using Application.Posts.Queries.GetComments;

namespace Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQuery : IGetAllPostsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PostDto>> Execute()
    {
        var posts = await _unitOfWork.Posts.GetAll();
        return posts.Select(p => new PostDto()
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            CreationDate = p.CreationDate,
            Comments = p.Comments.Select(c => new CommentDto()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                CreationDate = c.CreationDate
            }).ToList()
        }).ToList();
    }
}