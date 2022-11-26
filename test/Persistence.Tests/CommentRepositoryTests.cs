using Application.Comments.Commands.UpdateComment;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Persistence.Database;
using Persistence.Repository;
using Xunit;

namespace Persistence.Tests;

public class CommentRepositoryTests
{
    private readonly IQueryable<Comment> _comments;
    private readonly Mock<DbSet<Comment>> _dbSetMock;
    private readonly Mock<BlogContext> _blogContextMock;
    private readonly ICommentRepository _repository;
    
    public CommentRepositoryTests()
    {
        _comments = DatabaseInitializer.Comments.AsQueryable();
        _dbSetMock = _comments.BuildMockDbSet();
        _blogContextMock = new Mock<BlogContext>();
        _repository = new CommentRepository(_blogContextMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllComments()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);

        // act
        var comments = await _repository.GetAll();

        // assert
        comments.Count().Should().BePositive();
        comments.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_FindsComments_ReturnsExistingComment()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);
        var id = _comments.ElementAt(0).Id;

        // act
        var actual = await _repository.Get(id);

        // assert
        actual.HasValue.Should().Be(true);
        actual.Should().NotBeNull();
    }

    [Fact]

    public async Task Get_DoesNotFindComment_ReturnsEmptyMaybe()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);
        var id = Guid.Empty;

        // act
        var actual = await _repository.Get(id);

        // assert
        actual.HasNoValue.Should().Be(true);
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Update_NewContentIsProvided_CommentIsUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);
        var comment = _comments.ElementAt(0);
        
        var expectedContent = comment.Content + "1";
        var dto = new UpdateCommentDto { NewAuthor = string.Empty, NewContent = expectedContent };

        // act
        var actual = _repository.Update(comment, dto);

        // assert
        actual.Should().NotBeNull();
        expectedContent.Should().Be(actual.Content);
        actual.Author.Should().Be(comment.Author);
    }
    
    [Fact]
    public async Task Update_NewContentAndAuthorAreProvided_CommentIsUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);
        var comment = _comments.ElementAt(0);

        var expectedAuthor = comment.Author + "1";
        var expectedContent = comment.Content + "1";
        var dto = new UpdateCommentDto { NewAuthor = expectedAuthor, NewContent = expectedContent };

        // act
        var actual = _repository.Update(comment, dto);

        // assert
        actual.Should().NotBeNull();
        expectedContent.Should().Be(actual.Content);
        expectedAuthor.Should().Be(actual.Author);
    }

    [Fact]
    public void Update_NoContentOrAuthorAreProvided_CommentNotUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Comments).Returns(_dbSetMock.Object);
        var comment = _comments.ElementAt(0);
        
        var dto = new UpdateCommentDto { NewAuthor = string.Empty, NewContent = string.Empty };
        
        // act
        var actual = _repository.Update(comment, dto);
        
        // assert
        actual.Should().NotBeNull();
        actual.Author.Should().Be(comment.Author);
        actual.Content.Should().Be(comment.Content);
    }
}