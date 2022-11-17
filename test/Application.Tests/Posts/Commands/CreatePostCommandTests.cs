using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
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
        var input = new CreatePostDto()
        {
            Title = "CR7 interview",
            Content = "Ten Haag is bald",
            Comments = new List<CreateCommentDto>()
            {
                new()
                {
                    Author = "from cristiano ronaldo",
                    Content = "Man United sucks"
                }
            }
        };
        _unitOfWork.Setup(u => u.Posts.Create(It.IsAny<CreatePostDto>()));

        // act
        await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Posts.Create(It.IsAny<CreatePostDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}