using Application.Comments.Queries.GetAllComments;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Queries;

public class GetAllCommentsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly GetAllCommentsQuery _commentsQuery;

    public GetAllCommentsQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _commentsQuery = new GetAllCommentsQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_GetsCalled_ReturnsAllExistingComments()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.GetAll()).ReturnsAsync(GetComments());

        // act
        var actual = await _commentsQuery.Execute();

        // assert
        _unitOfWork.Verify(u => u.Comments.GetAll(), Times.Once);
        
        actual.Should().NotBeNull();
        actual.Should().NotBeEmpty();
    }

    private IEnumerable<CommentListDto> GetComments()
    {
        return new List<CommentListDto>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "author 1",
                Content = "content 1",
                PostId = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Author = "author 2",
                Content = "content 2",
                PostId = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            }
        };
    }
}