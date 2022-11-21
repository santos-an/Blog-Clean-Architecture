using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : ICreatePostCommand
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommand(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PostDto>> Execute(CreatePostDto dto)
    {
        var post = new Post()
        {
            Title = dto.Title,
            Content = dto.Content,
            Comments = dto.Comments.Select(c => new Comment()
            {
                Content = c.Content,
                Author = c.Author,
            }).ToList()
        };

        await _unitOfWork.Posts.Create(post);
        await _unitOfWork.CommitAsync();

        // var result = _mapper.Map<PostDto>(post);
        
        return Result.Ok(new PostDto()
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
        });
    }
}