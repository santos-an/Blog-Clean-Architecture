using Application.Posts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IGetPostsListQuery _query;

    public PostsController(IGetPostsListQuery query) => _query = query;

    [HttpGet]
    public async Task<IEnumerable<PostsListDto>> Get()
    {
        return await _query.Execute();
    }
}