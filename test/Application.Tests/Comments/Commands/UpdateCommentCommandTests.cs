using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Comments.Commands;

public class UpdateCommentCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdateCommentCommand _command;

    public UpdateCommentCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new UpdateCommentCommand(_unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_FindsComment_UpdatesComment()
    {
        // arrange
        var commentId = Guid.NewGuid();
        const string newAuthor = "updated author for a comment";
        const string newContent = "updated content for a given comment";
        
        var input = new UpdateCommentDto()
        {
            Id = commentId,
            NewAuthor = newAuthor,
            NewContent = newContent
        };
        var expected = new CommentDto()
        {
            Id = commentId,
            Author = newAuthor,
            Content = newContent,
            PostId = Guid.NewGuid(),
            CreationDate = DateTime.Today
        };
        
        _unitOfWork.Setup(u => u.Comments.Update(It.IsAny<UpdateCommentDto>())).ReturnsAsync(expected);
        _unitOfWork.Setup(u => u.CommitAsync());
        
        // act
        await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Comments.Update(It.IsAny<UpdateCommentDto>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        
        expected.Id.Should().Be(input.Id);
        expected.Author.Should().Be(input.NewAuthor);
        expected.Content.Should().Be(input.NewContent);
    }

    [Fact]
    public async Task Execute_DoesNotFindComment_ThrowsException()
    {
        // arrange
        var commentId = Guid.NewGuid();
        const string newAuthor = "updated author for a comment";
        const string newContent = "updated content for a given comment";
        
        var input = new UpdateCommentDto()
        {
            Id = commentId,
            NewAuthor = newAuthor,
            NewContent = newContent
        };

        _unitOfWork.Setup(u => u.Comments.Update(It.IsAny<UpdateCommentDto>())).Throws<EntityNotFoundException>();

        try
        {
            // act
            await _command.Execute(input);
        }
        catch (EntityNotFoundException)
        {
            _unitOfWork.Verify(u => u.Comments.Update(It.IsAny<UpdateCommentDto>()), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}