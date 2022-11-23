using Application.Interfaces;
using Application.Posts.Queries.GetPost;
using AutoMapper;

namespace Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQuery : IGetAllPostsQuery
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostsQuery(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PostDto>> Execute()
    {
        var posts = await _unitOfWork.Posts.GetAll();
        return posts.Select(post => 
                _mapper.Map<PostDto>(post))
            .ToList();
    }
}