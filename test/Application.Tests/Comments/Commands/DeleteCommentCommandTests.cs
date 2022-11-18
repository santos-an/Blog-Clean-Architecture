using Application.Comments.Commands.DeleteComment;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Commands;

public class DeleteCommentCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DeleteCommentCommand _command;

    public DeleteCommentCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new DeleteCommentCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsComment_DeletesComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>(new Comment()));
        _unitOfWork.Setup(u => u.Comments.Delete(It.IsAny<Comment>())).ReturnsAsync(true);

        // act
        var actual = await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Comments.Delete(It.IsAny<Comment>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        actual.IsSuccess.Should().Be(true);
    }
    
    [Fact]
    public async Task Execute_DoesNotFindsComment_DeletesComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>());

        // act
        var actual = await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Comments.Delete(It.IsAny<Comment>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        actual.IsFailure.Should().Be(true);
    }
}