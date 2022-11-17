using Application.Comments.Commands.DeleteComment;
using Application.Interfaces;
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
    public async Task Execute_GetsCalled_DeletesNewComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Delete(It.IsAny<Guid>()));
        _unitOfWork.Setup(u => u.CommitAsync());
        
        // act
        await _command.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Comments.Delete(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}