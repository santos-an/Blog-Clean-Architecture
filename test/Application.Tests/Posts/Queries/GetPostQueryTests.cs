using Application.Interfaces;
using Application.Posts.Queries.GetPost;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Queries;

public class GetPostQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetPostQuery _query;

    public GetPostQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _query = new GetPostQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPostId_ReturnsExistingsPost()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>(new Post()));

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        actual.IsSuccess.Should().Be(true);
    }
    
    [Fact]
    public async Task Execute_DoesNotFindPostId_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>());

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        actual.IsFailure.Should().Be(true);

    }
}