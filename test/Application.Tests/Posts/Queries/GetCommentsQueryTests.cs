using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using Application.Posts.Queries.GetComments;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Queries;

public class GetCommentsQueryTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly IGetCommentsQuery  _query;

    public GetCommentsQueryTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _query = new GetCommentsQuery(_mapper.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPostId_ReturnsComments()
    {
        // arrange
        _mapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(new CommentDto());
        _unitOfWork.Setup(u => u.Posts.GetComments(It.IsAny<Guid>())).ReturnsAsync(new Maybe<IEnumerable<Comment>>(new List<Comment> { new() }));

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.GetComments(It.IsAny<Guid>()), Times.Once);
        _mapper.Verify(m => m.Map<CommentDto>(It.IsAny<Comment>()), Times.Once);
        
        actual.IsSuccess.Should().Be(true);
    }
    
    [Fact]
    public async Task Execute_DoesNotFindPostId_ReturnsFail()
    {
        // arrange
        _mapper.Setup(m => m.Map<PostDto>(It.IsAny<Post>())).Returns(new PostDto());
        _unitOfWork.Setup(u => u.Posts.GetComments(It.IsAny<Guid>())).ReturnsAsync(new Maybe<IEnumerable<Comment>>());

        // act
        var actual = await _query.Execute(Guid.NewGuid());

        // assert
        _unitOfWork.Verify(u => u.Posts.GetComments(It.IsAny<Guid>()), Times.Once);
        _mapper.Verify(m => m.Map<PostDto>(It.IsAny<Post>()), Times.Never);
        
        actual.IsFailure.Should().Be(true);
    }
}