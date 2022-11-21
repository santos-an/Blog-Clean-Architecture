using Application.Comments.Commands.CreateComment;
using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Commands;

public class CreateCommentCommandTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateCommentCommand _command;

    public CreateCommentCommandTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new CreateCommentCommand(_mapper.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsPost_AddsNewComment()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>(new Post()));
        _unitOfWork.Setup(u => u.Comments.Create(It.IsAny<Post>(), It.IsAny<Comment>())); 
        _unitOfWork.Setup(u => u.CommitAsync());
        _mapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(new CommentDto());

        // act
        var actual = await _command.Execute(new CreateCommentDto());

        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Create(It.IsAny<Post>(),It.IsAny<Comment>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        _mapper.Verify(m => m.Map<CommentDto>(It.IsAny<Comment>()), Times.Once);
        
        actual.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Execute_DoesNotFindPost_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Posts.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Post>());
        
        // act
        var actual = await _command.Execute(new CreateCommentDto());
        
        // assert
        _unitOfWork.Verify(u => u.Posts.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Create(It.IsAny<Post>(),It.IsAny<Comment>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        _mapper.Verify(m => m.Map<CommentDto>(It.IsAny<Comment>()), Times.Never);
        actual.IsFailure.Should().Be(true);
    }
}