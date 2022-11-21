using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Queries;

public class GetAllCommentsQueryTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly GetAllCommentsQuery _commentsQuery;

    public GetAllCommentsQueryTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _commentsQuery = new GetAllCommentsQuery(_mapper.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_GetsCalled_ReturnsAllExistingComments()
    {
        // arrange
        _mapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(new CommentDto());
        _unitOfWork.Setup(u => u.Comments.GetAll()).ReturnsAsync(Comments());

        // act
        var actual = await _commentsQuery.Execute();

        // assert
        _unitOfWork.Verify(u => u.Comments.GetAll(), Times.Once);
        
        actual.Should().NotBeNull();
        actual.Should().NotBeEmpty();
    }

    private IEnumerable<Comment> Comments()
    {
        return new List<Comment>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "author 1",
                Content = "content 1",
                PostId = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            }
        };
    }
}