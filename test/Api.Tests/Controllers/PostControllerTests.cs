using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Comments.Queries.GetComment;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Application.Posts.Queries.GetPost;
using Domain.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Tests.Controllers;

public class PostControllerTests
{
    private readonly Mock<IGetAllPostsQuery> _allPostsQuery;
    private readonly Mock<IGetPostQuery> _singlePostQuery;
    private readonly Mock<IGetCommentsQuery> _commentsQuery;
    private readonly Mock<ICreatePostCommand> _createPostCommand;
    private readonly Mock<IDeletePostCommand> _deletePostCommand;
    private readonly Mock<IUpdatePostCommand> _updatePostCommand;

    private readonly PostsController _controller;

    public PostControllerTests()
    {
        _allPostsQuery = new Mock<IGetAllPostsQuery>();
        _singlePostQuery = new Mock<IGetPostQuery>();
        _commentsQuery = new Mock<IGetCommentsQuery>();
        _createPostCommand = new Mock<ICreatePostCommand>();
        _deletePostCommand = new Mock<IDeletePostCommand>();
        _updatePostCommand = new Mock<IUpdatePostCommand>();

        _controller = new PostsController(_allPostsQuery.Object, _singlePostQuery.Object, _commentsQuery.Object,
            _createPostCommand.Object, _updatePostCommand.Object, _deletePostCommand.Object);
    }

    [Fact]
    public async Task GetAll_Returns_ExistingPosts()
    {
        // arrange
        var expected = new List<PostDto>
        {
            new()
            {
                Id = Guid.NewGuid(), Title = "post title", Content = "post content", CreationDate = DateTime.Today,
                Comments = new List<CommentDto>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(), Author = "comment from authpr 1", Content = "content from author 1",
                        CreationDate = DateTime.Today
                    }
                }.ToList()
            }
        };
        _allPostsQuery.Setup(q => q.Execute()).ReturnsAsync(expected);

        // act
        var result = await _controller.GetAll();
        var actual = (result as OkObjectResult).Value;

        // assert
        _allPostsQuery.Verify(q => q.Execute(), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        actual.Should().Be(expected);
    }

    [Fact]
    public async Task Get_FindsPost_ReturnsExistingPost()
    {
        // arrange
        var expected = Result.Ok(new PostDto()
        {
            Id = Guid.NewGuid(),
            Title = "post title",
            Content = "post content",
            CreationDate = DateTime.Today
        });
        _singlePostQuery.Setup(q => q.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Get(Guid.NewGuid());
        var actual = (result as OkObjectResult).Value as PostDto;

        // assert
        _singlePostQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        actual.Should().NotBeNull();
        expected.Value.Id.Should().Be(actual.Id);
        expected.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Get_DoesFindsPost_ReturnsBadRequest()
    {
        // arrange
        var expected = Result.Fail<PostDto>("error");
        _singlePostQuery.Setup(q => q.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Get(Guid.NewGuid());
        var actual = (result as BadRequestObjectResult).Value;

        // assert
        _singlePostQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.Error.Should().Be(actual.ToString());
        expected.IsFailure.Should().Be(true);
    }

    [Fact]
    public async Task GetComments_FindsPostId_ReturnsExistingComments()
    {
        // arrange
        var expected = Result.Ok<IEnumerable<CommentDto>>(new List<CommentDto>()
        {
            new()
            {
                Id = Guid.NewGuid()
            }
        });
        _commentsQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var result = await _controller.GetComments(Guid.NewGuid());
        var actual = (result as OkObjectResult).Value;

        // assert
        _commentsQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        expected.IsSuccess.Should().Be(true);
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task GetComments_DoesNotFindsPost_ThrowsBadRequest()
    {
        // arrange
        var expected = Result.Fail<IEnumerable<CommentDto>>("error");
        _commentsQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var result = await _controller.GetComments(Guid.NewGuid());
        var actual = (result as BadRequestObjectResult).Value;

        // assert
        _commentsQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.IsFailure.Should().Be(true);
        actual.Should().Be(expected.Error);
    }

    [Fact]
    public async Task Create_AddsNewPost_ReturnsOk()
    {
        // arrange
        var expected = Result.Ok(new PostDto());
        _createPostCommand.Setup(c => c.Execute(It.IsAny<CreatePostDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Create(new CreatePostDto());
        var actual = (result as OkObjectResult).Value;

        // assert
        _createPostCommand.Verify(c => c.Execute(It.IsAny<CreatePostDto>()), Times.Once);
        result.Should().BeOfType<OkObjectResult>();
        expected.IsSuccess.Should().Be(true);
        expected.Value.Should().Be(actual);
    }

    [Fact]
    public async Task Create_FailsAddNewPost_ReturnsBadRequest()
    {
        // arrange
        var expected = Result.Fail<PostDto>("error");
        _createPostCommand.Setup(c => c.Execute(It.IsAny<CreatePostDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Create(new CreatePostDto());
        var actual = (result as BadRequestObjectResult).Value;

        // assert
        _createPostCommand.Verify(c => c.Execute(It.IsAny<CreatePostDto>()), Times.Once);
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.IsFailure.Should().Be(true);
        expected.Error.Should().Be(actual.ToString());
    }

    [Fact]
    public async Task Update_FindsPost_ReturnsOK()
    {
        // arrange
        var expected = Result.Ok(new PostDto()
        {
            Id = Guid.NewGuid(),
            Content = "new content of post 1, after update",
            Title = "post 1",
            CreationDate = DateTime.Today
        });
        _updatePostCommand.Setup(c => c.Execute(It.IsAny<UpdatePostDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Update(new UpdatePostDto());
        var actual = (result as OkObjectResult).Value;

        // assert
        _updatePostCommand.Verify(c => c.Execute(It.IsAny<UpdatePostDto>()), Times.Once);
        
        result.Should().BeOfType<OkObjectResult>();
        expected.IsSuccess.Should().Be(true);
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task Update_DoesNotFindsPost_ReturnsBadRequest()
    {
        // arrange
        var expected = Result.Fail<PostDto>("error");
        _updatePostCommand.Setup(c => c.Execute(It.IsAny<UpdatePostDto>())).ReturnsAsync(expected);

        // act
        var result = await _controller.Update(new UpdatePostDto());
        var actual = (result as BadRequestObjectResult).Value;

        // assert
        _updatePostCommand.Verify(c => c.Execute(It.IsAny<UpdatePostDto>()), Times.Once);
        
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.IsFailure.Should().Be(true);
        actual.Should().Be(expected.Error);
    }

    [Fact]
    public async Task Delete_FindsPost_DeletesPostAndReturnsOK()
    {
        // arrange
        var expected = Result.Ok(true);
        _deletePostCommand.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);
        
        // act
        var result = await _controller.Delete(Guid.NewGuid());
        var actual = (result as OkObjectResult).Value;
        
        // assert
        _deletePostCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        
        result.Should().BeOfType<OkObjectResult>();
        expected.IsSuccess.Should().Be(true);
        actual.Should().Be("Deleted");
    }
    
    [Fact]
    public async Task Delete_DoesNotFindsPost_ThrowsException()
    {
        // arrange
        var expected = Result.Fail<bool>("error");
        _deletePostCommand.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);
        
        // act
        var result = await _controller.Delete(Guid.NewGuid());
        var actual = (result as BadRequestObjectResult).Value;
        
        // assert
        _deletePostCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        
        result.Should().BeOfType<BadRequestObjectResult>();
        expected.IsFailure.Should().Be(true);
        actual.Should().Be("error");
    }
}