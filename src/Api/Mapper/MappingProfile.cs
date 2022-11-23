using Application.Comments.Queries.GetComment;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Domain.Entities;

namespace Api.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostDto>().ForMember(p => p.Comments, act => act.MapFrom(src => src.Comments));
        CreateMap<Comment, CommentDto>();
    }
}