using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Domain.Tests;

public class CommentTests
{
    private readonly Comment _comment;

    public CommentTests()
    {
        _comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Author = "new author",
            Content = "new content",
            PostId = Guid.NewGuid(),
            CreationDate = DateTime.Now
        };
    }

    [Fact]
    public void SetAndGetId_UpdatesEntity()
    {
        // arrange
        var expected = Guid.NewGuid();
        
        // act
        _comment.Id = expected;

        // assert
        expected.Should().Be(_comment.Id);
    }
    
    [Fact]
    public void SetAndGetAuthor_UpdatesEntity()
    {
        // arrange
        var expected = "new author";
        
        // act
        _comment.Author = expected;

        // assert
        expected.Should().Be(_comment.Author);
    }
    
    [Fact]
    public void SetAndGetContent_UpdatesEntity()
    {
        // arrange
        var expected = "new content";
        
        // act
        _comment.Content = expected;

        // assert
        expected.Should().Be(_comment.Content);
    }
    
    [Fact]
    public void SetAndGetPostId_UpdatesEntity()
    {
        // arrange
        var expected = Guid.NewGuid();
        
        // act
        _comment.PostId = expected;

        // assert
        expected.Should().Be(_comment.PostId);
    }
    
    [Fact]
    public void SetAndGetCreationDate_UpdatesEntity()
    {
        // arrange
        var expected = DateTime.Now;
        
        // act
        _comment.CreationDate = expected;

        // assert
        expected.Should().Be(_comment.CreationDate);
    }
}