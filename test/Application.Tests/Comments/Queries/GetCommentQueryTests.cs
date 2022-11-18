using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Queries;

public class GetCommentQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetCommentQuery _singleCommentQuery;

    public GetCommentQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _singleCommentQuery = new GetCommentQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsComment_ReturnsExistingComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>(new Comment()));
    
        // act
        var actual = await _singleCommentQuery.Execute(Guid.NewGuid());
    
        // assert
        _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
        actual.IsSuccess.Should().Be(true);
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Execute_DoesNotFindComment_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>());
    
        // act
        var actual = await _singleCommentQuery.Execute(Guid.NewGuid());
        
        // assert
        _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
        actual.IsFailure.Should().Be(true);
        actual.Should().NotBeNull();
    }
}