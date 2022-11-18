using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Application.Posts.Queries.GetSinglePost;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IGetAllPostsQuery _allPostsQuery;
    private readonly IGetPostQuery _postQuery;
    private readonly IGetCommentsQuery _commentsQuery;
    private readonly ICreatePostCommand _createPostCommand;
    private readonly IDeletePostCommand _deletePostCommand;
    private readonly IUpdatePostCommand _updatePostCommand;

    public PostsController(
        IGetAllPostsQuery allPostsQuery, 
        IGetPostQuery postQuery, 
        IGetCommentsQuery commentsQuery, 
        ICreatePostCommand createPostCommand, 
        IUpdatePostCommand updatePostCommand, 
        IDeletePostCommand deletePostCommand)
    {
        _allPostsQuery = allPostsQuery;
        _postQuery = postQuery;
        _commentsQuery = commentsQuery;
        _createPostCommand = createPostCommand;
        _updatePostCommand = updatePostCommand;
        _deletePostCommand = deletePostCommand;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _allPostsQuery.Execute();
        return Ok(posts);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result =  await _postQuery.Execute(id);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }

    [HttpGet]
    [Route("{id}/comments")]
    public async Task<IActionResult> GetComments(Guid id)
    {
        var result =  await _commentsQuery.Execute(id);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePostDto dto)
    {
        await _createPostCommand.Execute(dto);
        return Ok("Created");
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatePostDto dto)
    {
        var result = await _updatePostCommand.Execute(dto);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deletePostCommand.Execute(id);
        if (result.IsFailure)
            return BadRequest(result.Error);
    
        return Ok("Deleted");
    }
}