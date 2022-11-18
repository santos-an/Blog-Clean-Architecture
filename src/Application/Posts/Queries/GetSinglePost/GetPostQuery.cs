using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Domain.Common;

namespace Application.Posts.Queries.GetSinglePost;

public class GetPostQuery : IGetPostQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPostQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<PostDto>> Execute(Guid id)
    {
        var maybe = await _unitOfWork.Posts.Get(id);
        if (maybe.HasNoValue)
            return Result.Fail<PostDto>($"There is no post with the id:{id}");

        var post = maybe.Value;
        var dto = new PostDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreationDate = post.CreationDate,
            Comments = post.Comments.Select(c => new CommentDto()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                CreationDate = c.CreationDate
            }).ToList()
        };
        return Result.Ok(dto);
    }
}