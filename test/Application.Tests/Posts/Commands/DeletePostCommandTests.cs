using Application.Interfaces;
using Application.Posts.Commands.DeletePost;
using Domain.Exceptions;
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
        _unitOfWork.Setup(u => u.Posts.Delete(It.IsAny<Guid>()));

        // act
        await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.Delete(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ThrowsException()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Delete(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        try
        {
            // act
            await _command.Execute(Guid.NewGuid());
        }
        catch (EntityNotFoundException)
        {
            // assert
            _unitOfWork.Verify(u => u.Posts.Delete(It.IsAny<Guid>()), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}