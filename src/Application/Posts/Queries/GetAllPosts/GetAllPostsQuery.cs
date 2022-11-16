using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQuery : IGetAllPostsQuery
{
    private readonly IDatabaseService _database;

    public GetAllPostsQuery(IDatabaseService database) => _database = database;

    public async Task<List<PostListDto>> Execute()
    {
        var posts = _database.Posts
            .Include(p => p.Comments)
            .Select(p => new PostListDto()
            {
                Id = p.Id,
                Content = p.Content,
                Title = p.Title,
                CreationDate = p.CreationDate,
                Comments = p.Comments.Select(c => new CommentDto()
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    CreationDate = c.CreationDate
                }).ToList()
            });

        return await posts.ToListAsync();
    }
}