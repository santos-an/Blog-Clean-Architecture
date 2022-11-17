using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Application.Posts.Queries.GetSinglePost;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Tests.Controllers;

public class PostControllerTests
{
    private readonly Mock<IGetAllPostsQuery> _allPostsQuery;
    private readonly Mock<IGetSinglePostQuery> _singlePostQuery;
    private readonly Mock<IGetCommentsQuery> _commentsQuery;
    private readonly Mock<ICreatePostCommand> _createPostCommand;
    private readonly Mock<IDeletePostCommand> _deletePostCommand;
    private readonly Mock<IUpdatePostCommand> _updatePostCommand;

    private readonly PostsController _controller;

    public PostControllerTests()
    {
        _allPostsQuery = new Mock<IGetAllPostsQuery>();
        _singlePostQuery = new Mock<IGetSinglePostQuery>();
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
        var actual = await _controller.GetAll();
        
        // assert
        _allPostsQuery.Verify(q => q.Execute(), Times.Once);
        actual.Should().NotBeEmpty();
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_FindsPost_ReturnsExistingPost()
    {
        // arrange
        var expected = new PostDto()
        {
            Id = Guid.NewGuid(),
            Title = "post title",
            Content = "post content",
            CreationDate = DateTime.Today,
            Comments = new List<CommentDto>()
            {
                new()
                {
                    Id = Guid.NewGuid(), Author = "comment from authpr 1", Content = "content from author 1",
                    CreationDate = DateTime.Today
                }
            }.ToList()
        };
        _singlePostQuery.Setup(q => q.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);
        
        // act
        var actual = await _controller.Get(Guid.NewGuid());
        
        // assert
        _singlePostQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        actual.Should().NotBeNull();
        expected.Should().Be(actual);
    }

    [Fact]
    public async Task Get_DoesFindsPost_ThrowsException()
    {
        // arrange
        _singlePostQuery.Setup(q => q.Execute(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        PostDto actual = null;
        try
        {
            // act
            actual = await _controller.Get(Guid.NewGuid());
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _singlePostQuery.Verify(q => q.Execute(It.IsAny<Guid>()), Times.Once);
            actual.Should().BeNull();
        }
    }

    [Fact]
    public async Task GetComments_FindsPost_ReturnsExistingsComments()
    {
        // arrange
        var expected = new PostWithCommentsDto()
        {
            PostId = Guid.NewGuid(),
            Comments = new List<CommentListDto>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Author = "author 1",
                    Content = "content 1",
                    CreationDate = DateTime.Today
                }
            }
        };
        _commentsQuery.Setup(c => c.Execute(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var actual = await _controller.GetComments(Guid.NewGuid());
        
        // assert
        _commentsQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        expected.Should().Be(actual);
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetComments_DoesNotFindsPost_ThrowsException()
    {
        // arrange
        _commentsQuery.Setup(c => c.Execute(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        PostWithCommentsDto actual = null;
        try
        {
            // act
            actual = await _controller.GetComments(Guid.NewGuid());
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _commentsQuery.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
            actual.Should().BeNull();
        }
    }

    [Fact]
    public async Task Create_AddsNewPost_ReturnsOk()
    {
        // arrange
        _createPostCommand.Setup(c => c.Execute(It.IsAny<CreatePostDto>()));
        var input = new CreatePostDto()
        {
            Title = "new post title",
            Content = "new post content",
            Comments = new List<CreateCommentDto>()
            {
                new()
                {
                    Author = "new author",
                    Content = "new comment"
                }
            }
        };

        // act
        var actual = await _controller.Create(input);

        // assert
        _createPostCommand.Verify(c => c.Execute(It.IsAny<CreatePostDto>()), Times.Once);
        
        actual.Should().BeOfType<HttpResponseMessage>();
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Update_UpdatesPost_ReturnsOK()
    {
        // arrange
        var expected = new PostDto()
        {
            Id = Guid.NewGuid(),
            Content = "new content of post 1, after update",
            Title = "post 1",
            CreationDate = DateTime.Today,
            Comments = new List<CommentDto>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Author = "author 1",
                    Content = "content 1",
                    CreationDate = DateTime.Now
                }
            }
        };
        var input = new UpdatePostDto()
        {
            Id = Guid.NewGuid(),
            Content = "content of post 1",
            Title = "post 1"
        };
        _updatePostCommand.Setup(c => c.Execute(It.IsAny<UpdatePostDto>())).ReturnsAsync(expected);
        
        // act
        var actual = await _controller.Update(input);

        // assert
        _updatePostCommand.Verify(c => c.Execute(It.IsAny<UpdatePostDto>()), Times.Once);

        expected.Should().Be(actual);
        actual.Should().NotBeNull();
        expected.Title.Should().Be(actual.Title);
        expected.Content.Should().Be(actual.Content);
    }
    
    [Fact]
    public async Task Delete_FindsPost_DeletesPostAndReturnsOK()
    {
        // arrange
        _deletePostCommand.Setup(c => c.Execute(It.IsAny<Guid>()));
        
        // act
        var actual = await _controller.Delete(Guid.NewGuid());
        
        // assert
        _deletePostCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
        
        actual.Should().BeOfType<HttpResponseMessage>();
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Delete_DoesNotFindsPost_ThrowsException()
    {
        // arrange
        _deletePostCommand.Setup(c => c.Execute(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        PostDto actual = null;
        try
        {
            // act
            actual = await _controller.Get(Guid.NewGuid());
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _deletePostCommand.Verify(c => c.Execute(It.IsAny<Guid>()), Times.Once);
            actual.Should().BeNull();
        }
    }
}