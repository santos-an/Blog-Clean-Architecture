using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetSingleComment;
using Domain.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Tests.Controllers;

public class CommentControllerTests
{
    private readonly Mock<IGetAllCommentsQuery> _getAllCommentsQuery;
    private readonly Mock<IGetCommentQuery> _getCommentQuery;
    private readonly Mock<ICreateCommentCommand> _createCommentCommand;
    private readonly Mock<IUpdateCommentCommand> _updateCommentCommand;
    private readonly Mock<IDeleteCommentCommand> _deleteCommentCommand;

    private readonly CommentsController _controller;

    public CommentControllerTests()
    {
        _getAllCommentsQuery = new Mock<IGetAllCommentsQuery>();
        _getCommentQuery = new Mock<IGetCommentQuery>();
        _createCommentCommand = new Mock<ICreateCommentCommand>();
        _updateCommentCommand = new Mock<IUpdateCommentCommand>();
        _deleteCommentCommand = new Mock<IDeleteCommentCommand>();

        _controller = new CommentsController(
            _getAllCommentsQuery.Object, 
            _getCommentQuery.Object,
            _createCommentCommand.Object, 
            _updateCommentCommand.Object, 
            _deleteCommentCommand.Object);
    }

    [Fact]
    public async Task GetAll_Returns_ExistingComments()
    {
        // Arrange
        var expected = new List<CommentDto>()
        {
            new()
            {
                Author = "author 1", Content = "conten 1", PostId = Guid.NewGuid(), CommentId = Guid.NewGuid(),
                CreationDate = DateTime.Today
            }
        };
        _getAllCommentsQuery.Setup(p => p.Execute()).ReturnsAsync(expected);

        // Act
        var result = await _controller.GetAll();
        var actual = (result as OkObjectResult).Value;

        // Assert
        _getAllCommentsQuery.Verify(c => c.Execute(), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        actual.Should().Be(expected);
    }

    [Fact]
    public async Task Get_FindsComment_ReturnsExistingComment()
    {
        // Arrange
        var expected = Result.Ok(new CommentDto()
        {
            CommentId = Guid.NewGuid(),
            Author = "author 1",
            Content = "content 1",
            PostId = Guid.NewGuid(),
            CreationDate = DateTime.Today,
        });
        _getCommentQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // Act
        var result = await _controller.Get(Guid.NewGuid());
        var actual = (result as OkObjectResult).Value;

        // assert
        _getCommentQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        expected.Value.Should().Be(actual);
    }

    [Fact]
    public async Task Get_DoesNotFindComment_ThrowsBadRequest()
    {
        // arrange
        var expected = Result.Fail<CommentDto>($"There is no comment for the given id:{Guid.NewGuid()}");
        _getCommentQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Get(Guid.NewGuid());
        var actual = (result as BadRequestResult);

        // assert
        _getCommentQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        actual.Should().BeNull();
    }

    [Fact]
    public async Task Create_AddsNewComment_ReturnsOK()
    {
        // arrange
        var input = new CreateCommentDto { PostId = Guid.NewGuid(), Author = "author 1", Content = "content 1", };
        var expected = Result.Ok(new CommentDto { CommentId = Guid.NewGuid(), PostId = input.PostId, Author = input.Author, Content = input.Content });
        _createCommentCommand.Setup(c => c.Execute(It.IsAny<CreateCommentDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Create(input);
        var actual = (result as OkObjectResult).Value as CommentDto;

        // assert
        _createCommentCommand.Verify(c => c.Execute(It.IsAny<CreateCommentDto>()), Times.Once);
        
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        expected.Value.PostId.Should().Be(actual.PostId);
        expected.Value.Author.Should().Be(actual.Author);
        expected.Value.Content.Should().Be(actual.Content);
    }

    [Fact]
    public async Task Update_FindsComment_ReturnsOK()
    {
        // arrange
        var input = new UpdateCommentDto()
        {
            Id = Guid.NewGuid(),
            NewAuthor = "new author",
            NewContent = "new content"
        };
        var expected = Result.Ok(new CommentDto()
        {
            CommentId = input.Id,
            Author = input.NewAuthor,
            Content = input.NewContent
        });
        _updateCommentCommand.Setup(c => c.Execute(It.IsAny<UpdateCommentDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Update(input);
        var actual = (result as OkObjectResult).Value as CommentDto;
        
        // assert
        _updateCommentCommand.Verify(c => c.Execute(It.IsAny<UpdateCommentDto>()), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        expected.Value.PostId.Should().Be(actual.PostId);
        expected.Value.Author.Should().Be(actual.Author);
        expected.Value.Content.Should().Be(actual.Content);
    }

    [Fact]
    public async Task Update_DoesNotFindsComment_ReturnsBadRequest()
    {
        // arrange
        var input = new UpdateCommentDto { Id = Guid.NewGuid() };
        var expected = Result.Fail<CommentDto>("error");
        _updateCommentCommand.Setup(c => c.Execute(It.IsAny<UpdateCommentDto>())).ReturnsAsync(expected);
        
        // act
        var result = await _controller.Update(input);
        var actual = (result as BadRequestObjectResult).Value;
        
        // assert
        _updateCommentCommand.Verify(c => c.Execute(It.IsAny<UpdateCommentDto>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.Error.Should().Be(actual.ToString());
    }
    
    [Fact]
    public async Task Delete_FindsComment_DeletesCommentAndReturnsOK()
    {
        // arrange
        var expected = Result.Ok(true);
        _deleteCommentCommand.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);
        
        // act
        var result = await _controller.Delete(Guid.NewGuid());
        var actual = (result as OkObjectResult).Value;
        
        // assert
        _deleteCommentCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        expected.Value.Should().Be(true);
        actual.Should().Be("Deleted");
    }
    
    [Fact]
    public async Task Delete_DoesNotFindComment_ThrowsBadRequest()
    {
        // arrange
        var expected = Result.Fail<bool>("error");
        _deleteCommentCommand.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);
        
        // act
        var result = await _controller.Delete(Guid.NewGuid());
        var actual = (result as BadRequestObjectResult).Value;
        
        // assert
        _deleteCommentCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.Error.Should().Be(actual.ToString());
    }
}