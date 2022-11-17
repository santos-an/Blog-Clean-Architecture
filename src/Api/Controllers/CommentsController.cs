using System.Net;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetByPostId;
using Application.Comments.Queries.GetSingleComment;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IGetAllCommentsQuery _getAllCommentsQuery;
    private readonly IGetSingleCommentQuery _getSingleCommentQuery;
    private readonly ICreateCommentCommand _createCommentCommand;
    private readonly IUpdateCommentCommand _updateCommentCommand;
    private readonly IDeleteCommentCommand _deleteCommentCommand;
    private readonly IGetCommentByPostIdQuery _commentByPostIdQuery;

    public CommentsController(
        IGetAllCommentsQuery getAllCommentsQuery,
        IGetSingleCommentQuery getSingleCommentQuery,
        ICreateCommentCommand createCommentCommand,
        IUpdateCommentCommand updateCommentCommand,
        IDeleteCommentCommand deleteCommentCommand,
        IGetCommentByPostIdQuery commentByPostIdQuery)
    {
        _getAllCommentsQuery = getAllCommentsQuery;
        _getSingleCommentQuery = getSingleCommentQuery;
        _createCommentCommand = createCommentCommand;
        _updateCommentCommand = updateCommentCommand;
        _deleteCommentCommand = deleteCommentCommand;
        _commentByPostIdQuery = commentByPostIdQuery;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CommentListDto>> GetAll() => await _getAllCommentsQuery.Execute();
    
    [HttpGet]
    [Route("{id}")]
    public async Task<CommentDto> Get(Guid id) => await _getSingleCommentQuery.Execute(id);
    
    [HttpGet]
    [Route("GetByPostId/{id}")]
    public async Task<IEnumerable<CommentListDto>> GetByPostId(Guid id) => await _commentByPostIdQuery.Execute(id);
    
    [HttpPost]
    public async Task<HttpResponseMessage> Create(CreateCommentDto dto)
    {
        await _createCommentCommand.Execute(dto);
    
        return new HttpResponseMessage(HttpStatusCode.Created);
    }
    
    [HttpPut]
    public async Task<HttpResponseMessage> Update(UpdateCommentDto dto)
    {
        await _updateCommentCommand.Execute(dto);
    
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<HttpResponseMessage> Delete(Guid id)
    {
        await _deleteCommentCommand.Execute(id);
    
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}