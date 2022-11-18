using Application.Interfaces;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Domain.Common;
using Domain.Entities;
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
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>(new Post()));
        _unitOfWork.Setup(u => u.Posts.Update(It.IsAny<Post>(), It.IsAny<UpdatePostDto>())).Returns(new Post());
        
        // act
        var actual = await _command.Execute(new UpdatePostDto());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Posts.Update(It.IsAny<Post>(), It.IsAny<UpdatePostDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);

        actual.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ThrowsException()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>());
        
        // act
        var actual = await _command.Execute(new UpdatePostDto());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Posts.Update(It.IsAny<Post>(), It.IsAny<UpdatePostDto>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);

        actual.IsFailure.Should().Be(true);
    }
}