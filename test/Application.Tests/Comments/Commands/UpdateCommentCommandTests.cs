using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Commands;

public class UpdateCommentCommandTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdateCommentCommand _command;

    public UpdateCommentCommandTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new UpdateCommentCommand(_mapper.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsComment_UpdatesComment()
    {
        // arrange
        _mapper.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(new CommentDto());
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>(new Comment()));
        _unitOfWork.Setup(u => u.Comments.Update(It.IsAny<Comment>(), It.IsAny<UpdateCommentDto>())).Returns(new Comment());
        
        // act
        var actual = await _command.Execute(new UpdateCommentDto());

        // assert
        _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Update(It.IsAny<Comment>(), It.IsAny<UpdateCommentDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        _mapper.Verify(m => m.Map<CommentDto>(It.IsAny<Comment>()), Times.Once);

        actual.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task Execute_DoesNotFindComment_ReturnsFail()
    {
        // arrange
        _unitOfWork.Setup(u => u.Comments.Get(It.IsAny<Guid>())).ReturnsAsync(new Maybe<Comment>());
        
        // act
        var actual = await _command.Execute(new UpdateCommentDto());
        
        // assert
        _unitOfWork.Verify(u => u.Comments.Get(It.IsAny<Guid>()), Times.Once);
        _unitOfWork.Verify(u => u.Comments.Update(It.IsAny<Comment>(), It.IsAny<UpdateCommentDto>()), Times.Never);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        _mapper.Verify(m => m.Map<CommentDto>(It.IsAny<Comment>()), Times.Never);

        actual.IsFailure.Should().Be(true);
    }
}