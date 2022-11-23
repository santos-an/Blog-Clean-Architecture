using Api.Utils;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetComment;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IGetAllCommentsQuery _getAllCommentsQuery;
    private readonly IGetCommentQuery _getCommentQuery;
    private readonly ICreateCommentCommand _createCommentCommand;
    private readonly IUpdateCommentCommand _updateCommentCommand;
    private readonly IDeleteCommentCommand _deleteCommentCommand;

    public CommentsController(
        IGetAllCommentsQuery getAllCommentsQuery,
        IGetCommentQuery getCommentQuery,
        ICreateCommentCommand createCommentCommand,
        IUpdateCommentCommand updateCommentCommand,
        IDeleteCommentCommand deleteCommentCommand)
    {
        _getAllCommentsQuery = getAllCommentsQuery;
        _getCommentQuery = getCommentQuery;
        _createCommentCommand = createCommentCommand;
        _updateCommentCommand = updateCommentCommand;
        _deleteCommentCommand = deleteCommentCommand;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _getAllCommentsQuery.Execute();
        return Ok(comments);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _getCommentQuery.Execute(id);
        if (result.IsFailure)
            return Failure(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentDto dto)
    {
        var result = await _createCommentCommand.Execute(dto);
        if (result.IsFailure)
            return Failure(result.Error);

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCommentDto dto)
    {
        var result = await _updateCommentCommand.Execute(dto);
        if (result.IsFailure)
            return Failure(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteCommentCommand.Execute(id);
        if (result.IsFailure)
            return Failure(result.Error);

        return Ok("Deleted");
    }

    private BadRequestObjectResult Failure(string error)
    {
        return BadRequest(new ErrorDetails
        {
            StatusCode = BadRequest().StatusCode,
            Message = error
        });
    }
}