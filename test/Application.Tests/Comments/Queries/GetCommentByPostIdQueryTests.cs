using Application.Comments.Queries.GetByPostId;
using Application.Interfaces;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Queries;

public class GetCommentByPostIdQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly GetCommentByPostIdQuery _getCommentByPostIdQuery;

    public GetCommentByPostIdQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _getCommentByPostIdQuery = new GetCommentByPostIdQuery(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPosts_ReturnsExistingCommentFromThatPost()
    {
        // arrange

        // act
        
        // assert
    }
}