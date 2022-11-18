using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Application.Posts.Queries.GetComments;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Queries;

public class GetCommentsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetCommentsQuery  _query;

    public GetCommentsQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _query = new GetCommentsQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPostId_ReturnsComments()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.GetComments(It.IsAny<Guid>())).ReturnsAsync(new Maybe<IEnumerable<Comment>>(new List<Comment>()));

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.GetComments(It.IsAny<Guid>()), Times.Once);
        actual.IsSuccess.Should().Be(true);
    }
    
    [Fact]
    public async Task Execute_DoesNotFindPostId_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.GetComments(It.IsAny<Guid>())).ReturnsAsync(new Maybe<IEnumerable<Comment>>());

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.GetComments(It.IsAny<Guid>()), Times.Once);
        actual.IsFailure.Should().Be(true);
    }
}