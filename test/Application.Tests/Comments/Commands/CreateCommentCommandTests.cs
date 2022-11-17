using Application.Comments.Commands.CreateComment;
using Application.Interfaces;
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
    public async Task Execute_GetsCalled_CreatesNewComment()
    {
        // arrange
        var input = new CreateCommentDto()
        {
            PostId = Guid.NewGuid(),
            Author = "new author",
            Content = "new comment content"
        };
        _unitOfWork.Setup(u => u.Comments.Create(It.IsAny<CreateCommentDto>()));
        _unitOfWork.Setup(u => u.CommitAsync());

        // act
        await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Comments.Create(It.IsAny<CreateCommentDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}