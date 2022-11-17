using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Queries;

public class GetSingleSingleCommentQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetSingleCommentQuery _singleCommentQuery;

    public GetSingleSingleCommentQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _singleCommentQuery = new GetSingleSingleCommentQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsComment_ReturnsExistingComment()
    {
        // arrange
        var id = Guid.NewGuid();
        var expected = new CommentDto()
        {
            Id = id,
            Author = "comment from author 1",
            Content = "comment about world cup",
            PostId = Guid.NewGuid(),
            CreationDate = DateTime.Today
        };
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(expected);

        // act
        var actual = await _singleCommentQuery.Execute(id);

        // assert
        _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
        actual.Should().NotBeNull();
        expected.Id.Should().Be(actual.Id);
    }

    [Fact]
    public async Task Execute_DoesNotFindComment_ThrowsException()
    {
        // arrange
        var id = Guid.NewGuid();
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).Throws<EntityNotFoundException>();

        CommentDto actual = null;
        try
        {
            actual = await _singleCommentQuery.Execute(id);
        }
        catch (EntityNotFoundException e)
        {
            // assert
            _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
            actual.Should().BeNull();
        }
    }
}