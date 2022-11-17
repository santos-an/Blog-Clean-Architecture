using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Domain.Tests;

public class PostTests
{
    private readonly Post _post;
    
    public PostTests()
    {
        _post = new Post()
        {
            Id = Guid.NewGuid(),
            Content = string.Empty,
            Title = string.Empty,
            CreationDate = DateTime.Now,
            Comments = new List<Comment>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    Author = "author 1",
                    Content = "content 1",
                    CreationDate = DateTime.Now
                }
            }
        };
    }

    [Fact]
    public void SetAndGetId_UpdatesEntity()
    {
        // arrange
        var expected = Guid.NewGuid();
        
        // act
        _post.Id = expected;

        // assert
        expected.Should().Be(_post.Id);
    }

    [Fact]
    public void SetAndGetTitle_UpdatesEntity()
    {
        // arrange
        var expected = "new title";
        
        // act
        _post.Title = expected;

        // assert
        expected.Should().Be(_post.Title);
    }

    [Fact]
    public void SetAndGetContent_UpdatesEntity()
    {
        // arrange
        var expected = "new content";
        
        // act
        _post.Content = expected;

        // assert
        expected.Should().Be(_post.Content);
    }
    
    [Fact]
    public void SetAndGetCreationDate_UpdatesEntity()
    {
        // arrange
        var expected = DateTime.Today;
        
        // act
        _post.CreationDate = expected;

        // assert
        expected.Should().Be(_post.CreationDate);
    }
    
    [Fact]
    public void SetAndGetComments_UpdatesEntity()
    {
        // arrange
        var expected = new List<Comment>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "new author 1",
                Content = "about this interview",
                CreationDate = DateTime.Today,
                PostId = Guid.NewGuid()
            }
        };
        
        // act
        _post.Comments = expected;

        // assert
        expected.Should().NotBeEmpty();
    }
}