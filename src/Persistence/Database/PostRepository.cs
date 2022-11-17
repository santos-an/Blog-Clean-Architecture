using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using CommentDto = Application.Posts.Queries.GetAllPosts.CommentDto;

namespace Persistence.Database;

public class PostRepository : IPostRepository
{
    private readonly BlogContext _context;

    public PostRepository(BlogContext context) => _context = context;

    public async Task<IEnumerable<PostDto>> GetAll()
    {
        var posts = _context.Posts
            .Include(p => p.Comments)
            .Select(post => new PostDto()
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                CreationDate = post.CreationDate,
                Comments = post.Comments.Select(c => new CommentDto()
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    CreationDate = c.CreationDate
                }).ToList()
            });

        return await posts.ToListAsync();
    }

    public async Task<PostDto> Get(Guid id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            throw new EntityNotFoundException($"There is no post with id:{id} found on the database");
        
        return new PostDto()
        {
            Id = post.Id,
            Comments = post.Comments.Select(comment => new CommentDto()
            {
                Author = comment.Author,
                Content = comment.Content,
                Id = comment.Id,
                CreationDate = comment.CreationDate
            }).ToList(),
            Content = post.Content,
            Title = post.Title,
            CreationDate = post.CreationDate
        };
    }

    public async Task<PostWithCommentsDto> GetComments(Guid id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            throw new EntityNotFoundException($"There is no post with id:{id} found on the database");

        return new PostWithCommentsDto()
        {
            PostId = post.Id,
            Comments = post.Comments.Select(c => new CommentListDto()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                CreationDate = c.CreationDate
            }).ToList()
        };
    }

    public async Task Create(CreatePostDto dto)
    {
        var post = new Post()
        {
            Title = dto.Title,
            Content = dto.Content,
            CreationDate = DateTime.Now,
            Comments = dto.Comments.Select(comment => new Comment()
            {
                Content = comment.Content,
                Author = comment.Author,
                CreationDate = DateTime.Now
            }).ToList()
        };

        await _context.Posts.AddAsync(post);
    }

    public async Task<PostDto> Update(UpdatePostDto dto)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == dto.Id);
        if (post == null)
            throw new EntityNotFoundException($"There is no post with the given id:{dto.Id}");

        if (!string.IsNullOrEmpty(dto.Content))
            post.Content = dto.Content;
        if (!string.IsNullOrEmpty(dto.Title))
            post.Title = dto.Title;

        return new PostDto()
        {
            Id = post.Id,
            Content = post.Content,
            Title = post.Title,
            CreationDate = post.CreationDate,
            Comments = post.Comments.Select(comment => new CommentDto()
            {
                Id = comment.Id,
                Author = comment.Author,
                Content = comment.Content,
                CreationDate = comment.CreationDate
            }).ToList()
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
            throw new EntityNotFoundException($"There is no post with the given id:{id}");

        _context.Posts.Remove(post);
        return true;
    }
}