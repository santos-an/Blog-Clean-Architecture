using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Queries;

public class GetAllPostsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetAllPostsQuery  _query;

    public GetAllPostsQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _query = new GetAllPostsQuery(_unitOfWork.Object);
    }
    
    [Fact]
    public async Task Execute_GetsCalled_ReturnsAllExistingPosts()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.GetAll()).ReturnsAsync(Posts());
        
        // act
        var actual = await _query.Execute();
        
        // assert
        _unitOfWork.Verify(u => u.Posts.GetAll(), Times.Once);
        actual.Should().NotBeNull();
        actual.Should().NotBeEmpty();
    }

    private IEnumerable<Post> Posts()
    {
        return new List<Post>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Comments = new List<Comment>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Author = "author 1",
                        Content = "content 1",
                        PostId = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                    }
                }
            }
        };
    }
}