using Application.Interfaces;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Commands;

public class UpdatePostCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdatePostCommand _command;

    public UpdatePostCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new UpdatePostCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindPost_UpdatesPost()
    {
        // arrange
        var input = new UpdatePostDto()
        {
            Id = Guid.NewGuid(),
            Content = "new content",
            Title = "cr7"
        };
        var expected = new PostDto()
        {
            Id = Guid.NewGuid(),
            Title = input.Title,
            Content = input.Content,
            CreationDate = DateTime.Today,
            Comments = new List<CommentDto>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Author = "author 1",
                    Content = "content 1",
                    CreationDate = DateTime.Now,
                }
            }
        };
        _unitOfWork.Setup(u => u.Posts.Update(It.IsAny<UpdatePostDto>())).ReturnsAsync(expected);
        
        // act
        var actual = await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Posts.Update(It.IsAny<UpdatePostDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);

        expected.Id.Should().Be(actual.Id);
        expected.Title.Should().Be(actual.Title);
        expected.Content.Should().Be(actual.Content);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ThrowsException()
    {
        // arrange
        var input = new UpdatePostDto()
        {
            Id = Guid.NewGuid(),
            Content = "new content",
            Title = "cr7"
        };
        _unitOfWork.Setup(u => u.Posts.Update(It.IsAny<UpdatePostDto>())).Throws<EntityNotFoundException>();

        PostDto actual = null;
        try
        {
            // act
            actual = await _command.Execute(input);
        }
        catch (EntityNotFoundException)
        {
            // assert
            _unitOfWork.Verify(u => u.Posts.Update(It.IsAny<UpdatePostDto>()), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never());
            
            actual.Should().BeNull();
        }
    }
}