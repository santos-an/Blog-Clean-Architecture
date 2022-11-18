using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Commands;

public class CreatePostCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreatePostCommand _command;

    public CreatePostCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new CreatePostCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_GetsCalled_CreatesNewPostAndCommitsChanges()
    {
        // arrange
        var input = new CreatePostDto { Comments = new List<CreateCommentDto>() };
        _unitOfWork.Setup(u => u.Posts.Create(It.IsAny<Post>()));

        // act
        await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Posts.Create(It.IsAny<Post>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}