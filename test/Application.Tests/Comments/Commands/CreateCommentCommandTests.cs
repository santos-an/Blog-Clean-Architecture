using Application.Comments.Commands.CreateComment;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Commands;

public class CreateCommentCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateCommentCommand _command;

    public CreateCommentCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new CreateCommentCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPost_AddsNewComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>(new Post()));
        _unitOfWork.Setup(u => u.Comments.Create(It.IsAny<Post>(), It.IsAny<Comment>())); 
        _unitOfWork.Setup(u => u.CommitAsync());

        // act
        var actual = await _command.Execute(new CreateCommentDto());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Create(It.IsAny<Post>(),It.IsAny<Comment>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        actual.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>());
        
        // act
        var actual = await _command.Execute(new CreateCommentDto());
        
        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Create(It.IsAny<Post>(),It.IsAny<Comment>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        actual.IsFailure.Should().Be(true);
    }
}