using Application.Posts.Queries.GetAllPosts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IGetAllPostsQuery _getAllPostsQuery;

    public PostsController(IGetAllPostsQuery getAllPostsQuery) => _getAllPostsQuery = getAllPostsQuery;

    [HttpGet]
    public async Task<IEnumerable<PostListDto>> GetAll() => await _getAllPostsQuery.Execute();

    [HttpGet]
    [Route("{id}")]
    public async Task Get(Guid id)
    {
        
    }
    
    [HttpGet]
    [Route("{id}/comments")]
    public async Task GetComments(Guid id)
    {
        
    }

    [HttpPost]
    public async Task Create()
    {
        
    }

    [HttpPut]
    public async Task Update()
    {
        
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task Delete(Guid id)
    {
        
    }
}