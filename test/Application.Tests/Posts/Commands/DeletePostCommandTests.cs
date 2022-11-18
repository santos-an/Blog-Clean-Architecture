using Application.Interfaces;
using Application.Posts.Commands.DeletePost;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Commands;

public class DeletePostCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DeletePostCommand _command;

    public DeletePostCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new DeletePostCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPost_DeletesPost()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>(new Post()));
        _unitOfWork.Setup(u => u.Posts.Delete(It.IsAny<Post>())).ReturnsAsync(true);

        // act
        var actual = await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Posts.Delete(It.IsAny<Post>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        
        actual.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>());
    
        // act
        var actual = await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Posts.Delete(It.IsAny<Post>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        
        actual.IsFailure.Should().Be(true);
    }
}