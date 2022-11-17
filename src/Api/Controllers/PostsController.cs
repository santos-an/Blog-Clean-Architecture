using System.Net;
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
    private readonly IGetSinglePostQuery _singlePostQuery;
    private readonly IGetCommentsQuery _commentsQuery;
    private readonly ICreatePostCommand _createPostCommand;
    private readonly IDeletePostCommand _deletePostCommand;
    private readonly IUpdatePostCommand _updatePostCommand;

    public PostsController(
        IGetAllPostsQuery allPostsQuery, 
        IGetSinglePostQuery singlePostQuery, 
        IGetCommentsQuery commentsQuery, 
        ICreatePostCommand createPostCommand, 
        IUpdatePostCommand updatePostCommand, 
        IDeletePostCommand deletePostCommand)
    {
        _allPostsQuery = allPostsQuery;
        _singlePostQuery = singlePostQuery;
        _commentsQuery = commentsQuery;
        _createPostCommand = createPostCommand;
        _updatePostCommand = updatePostCommand;
        _deletePostCommand = deletePostCommand;
    }

    [HttpGet]
    public async Task<IEnumerable<PostDto>> GetAll() => await _allPostsQuery.Execute();

    [HttpGet]
    [Route("{id}")]
    public async Task<PostDto> Get(Guid id) => await _singlePostQuery.Execute(id);

    [HttpGet]
    [Route("{id}/comments")]
    public async Task<PostWithCommentsDto> GetComments(Guid id) => await _commentsQuery.Execute(id);

    [HttpPost]
    public async Task<HttpResponseMessage> Create(CreatePostDto dto)
    {
        await _createPostCommand.Execute(dto);
        
        return new HttpResponseMessage(HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<PostDto> Update(UpdatePostDto dto) => await _updatePostCommand.Execute(dto);

    [HttpDelete]
    [Route("{id}")]
    public async Task<HttpResponseMessage> Delete(Guid id)
    {
        await _deletePostCommand.Execute(id);
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}