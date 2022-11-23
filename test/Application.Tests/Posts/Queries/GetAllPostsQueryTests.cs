using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Queries;

public class GetAllPostsQueryTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetAllPostsQuery  _query;

    public GetAllPostsQueryTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _query = new GetAllPostsQuery(_mapper.Object, _unitOfWork.Object);
    }
    
    [Fact]
    public async Task Execute_GetsCalled_ReturnsAllExistingPosts()
    {
        // arrange
        _mapper.Setup(m => m.Map<PostDto>(It.IsAny<Post>())).Returns(new PostDto());
        _unitOfWork.Setup(u => u.Posts.GetAll()).ReturnsAsync(new List<Post>() { new()});
        
        // act
        var actual = await _query.Execute();
        
        // assert
        _unitOfWork.Verify(u => u.Posts.GetAll(), Times.Once);
        _mapper.Verify(m => m.Map<PostDto>(It.IsAny<Post>()), Times.Once);
        
        actual.Should().NotBeNull();
        actual.Should().NotBeEmpty();
    }
}