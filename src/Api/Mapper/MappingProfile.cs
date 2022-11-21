using Application.Comments.Queries.GetComment;
using Application.Posts.Queries.GetAllPosts;
using AutoMapper;
using Domain.Entities;

namespace Api.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<Comment, CommentDto>();
    }
}