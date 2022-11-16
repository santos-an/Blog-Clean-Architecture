using Application.Posts.Queries.GetAllPosts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IGetAllPostsQuery _getAllQuery;

    public PostsController(IGetAllPostsQuery getAllQuery) => _getAllQuery = getAllQuery;

    [HttpGet]
    public async Task<IEnumerable<PostListDto>> GetAll()
    {
        return await _getAllQuery.Execute();
    }
}