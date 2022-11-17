using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetByPostId;
using Application.Comments.Queries.GetSingleComment;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Tests;

public class CommentControllerTests
{
    private readonly Mock<IGetAllCommentsQuery> _getAllCommentsQuery;
    private readonly Mock<IGetSingleCommentQuery> _getSingleCommentQuery;
    private readonly Mock<ICreateCommentCommand> _createCommentCommand;
    private readonly Mock<IUpdateCommentCommand> _updateCommentCommand;
    private readonly Mock<IDeleteCommentCommand> _deleteCommentCommand;
    private readonly Mock<IGetCommentByPostIdQuery> _commentByPostIdQuery;

    private readonly CommentsController _controller;

    public CommentControllerTests()
    {
        _getAllCommentsQuery = new Mock<IGetAllCommentsQuery>();
        _getSingleCommentQuery = new Mock<IGetSingleCommentQuery>();
        _createCommentCommand = new Mock<ICreateCommentCommand>();
        _updateCommentCommand = new Mock<IUpdateCommentCommand>();
        _deleteCommentCommand = new Mock<IDeleteCommentCommand>();
        _commentByPostIdQuery = new Mock<IGetCommentByPostIdQuery>();

        _controller = new CommentsController(_getAllCommentsQuery.Object, _getSingleCommentQuery.Object, _createCommentCommand.Object, _updateCommentCommand.Object, _deleteCommentCommand.Object, _commentByPostIdQuery.Object);
    }

    [Fact]
    public async Task GetAll_Returns_ExistingComments()
    {
        // Arrange
        var expected = new List<CommentListDto>() { new() { Author = "author 1", Content = "conten 1", PostId = Guid.NewGuid(), Id = Guid.NewGuid(), CreationDate = DateTime.Today }};
        _getAllCommentsQuery.Setup(p => p.Execute()).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetAll();

        // Assert
        _getAllCommentsQuery.Verify(c => c.Execute(), Times.Once);
        actual.Should().NotBeEmpty();
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_FindsComment_ReturnsExistingComment()
    {
        // Arrange
        var expected = new CommentDto()
        {
            Id = Guid.NewGuid(),
            Author = "author 1",
            Content = "content 1",
            PostId = Guid.NewGuid(),
            CreationDate = DateTime.Today,
        };
        _getSingleCommentQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.Get(Guid.NewGuid());
        
        // assert
        _getSingleCommentQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        actual.Should().NotBeNull();
        expected.Should().Be(actual);
    }
    
    [Fact]
    public async Task Get_DoesNotFindComment_ThrowsException()
    {
        // arrange
        _getSingleCommentQuery.Setup(c => c.Execute(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
        
        // Act
        try
        {
            var actual = await _controller.Get(Guid.NewGuid());
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _getSingleCommentQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        }
    }

    [Fact]
    public async Task Create_AddsNewComment_ReturnsOK()
    {
        // arrange
        _createCommentCommand.Setup(C => C.Execute(It.IsAny<CreateCommentDto>()));
        var input = new CreateCommentDto()
        {
            PostId = Guid.NewGuid(),
            Author = "author 1",
            Content = "content 1",
        };
        
        // act
        var actual = await _controller.Create(input);
        
        // assert
        _createCommentCommand.Verify(c => c.Execute(It.IsAny<CreateCommentDto>()), Times.Once);
        
        actual.Should().BeOfType<HttpResponseMessage>();
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Update_UpdatesComment_ReturnsOK()
    {
        // arrange
        _updateCommentCommand.Setup(c => c.Execute(It.IsAny<UpdateCommentDto>()));
        var input = new UpdateCommentDto()
        {
            Id = Guid.NewGuid(),
            NewAuthor = "new author",
            NewContent = "new content"
        };
        
        // act
        var actual = await _controller.Update(input);
        
        // assert
        _updateCommentCommand.Verify(c => c.Execute(It.IsAny<UpdateCommentDto>()), Times.Once);

        actual.Should().BeOfType<HttpResponseMessage>();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Delete_FindsComment_DeletesCommentAndReturnsOK()
    {
        // arrange
        _deleteCommentCommand.Setup(c => c.Execute(It.IsAny<Guid>()));
        
        // act
        var actual = await _controller.Delete(Guid.NewGuid());
        
        // assert
        _deleteCommentCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);

        actual.Should().BeOfType<HttpResponseMessage>();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Delete_DoesNotFindComment_ThrowsException()
    {
        // arrange
        _deleteCommentCommand.Setup(c => c.Execute(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        try
        {
            // act
            var actual = await _controller.Delete(Guid.NewGuid());
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _deleteCommentCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        }
    }
}