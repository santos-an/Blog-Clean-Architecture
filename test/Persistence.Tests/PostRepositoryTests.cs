using Application.Interfaces;
using Application.Posts.Commands.UpdatePost;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Persistence.Database;
using Xunit;

namespace Persistence.Tests;

public class PostRepositoryTests
{
    private readonly IQueryable<Post> _posts;
    private readonly Mock<DbSet<Post>> _dbSetMock;
    private readonly Mock<BlogContext> _blogContextMock;
    private readonly IPostRepository _repository;

    public PostRepositoryTests()
    {
        _posts = DatabaseInitializer.Posts.AsQueryable();
        _dbSetMock = _posts.BuildMockDbSet();
        _blogContextMock = new Mock<BlogContext>();
        _repository = new PostRepository(_blogContextMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllPosts()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);

        // act
        var posts = await _repository.GetAll();

        // assert
        posts.Count().Should().BePositive();
        posts.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_FindsPost_ReturnsExistingPost()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var id = _posts.ElementAt(0).Id;

        // act
        var actual = await _repository.Get(id);

        // assert
        actual.HasValue.Should().Be(true);
        actual.Should().NotBeNull();
    }

    [Fact]

    public async Task Get_DoesNotFindPost_ReturnsEmptyMaybe()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var id = Guid.Empty;

        // act
        var actual = await _repository.Get(id);

        // assert
        actual.HasNoValue.Should().Be(true);
        actual.Should().NotBeNull();
    }

    [Fact]
    public async Task GetComments_FindsPost_ReturnsComments()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var id = _posts.ElementAt(0).Id;
        
        // act
        var actual = await _repository.GetComments(id);
        
        // assert
        actual.HasValue.Should().Be(true);
        actual.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetComments_DoesNotFindPost_ReturnsEmptyMaybe()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var id = Guid.Empty;
        
        // act
        var actual = await _repository.GetComments(id);
        
        // assert
        actual.HasNoValue.Should().Be(true);
    }

    [Fact]
    public async Task Update_NewContentIsProvided_PostIsUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var post = _posts.ElementAt(0);
        var expectedContent = post.Content + "1";
        var dto = new UpdatePostDto { Title = string.Empty, Content = expectedContent };

        // act
        var actual = _repository.Update(post, dto);

        // assert
        actual.Should().NotBeNull();
        expectedContent.Should().Be(actual.Content);
        actual.Title.Should().Be(post.Title);
    }
    
    [Fact]
    public void Update_NewContentAndNewTitleAreProvided_PostIsUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var post = _posts.ElementAt(0);

        var expectedTitle = post.Title + "1";
        var expectedContent = post.Content + "1";
        var dto = new UpdatePostDto { Title = expectedTitle, Content = expectedContent };

        // act
        var actual = _repository.Update(post, dto);

        // assert
        actual.Should().NotBeNull();
        expectedContent.Should().Be(actual.Content);
        expectedTitle.Should().Be(actual.Title);
    }
    
    [Fact]
    public void Update_NoContentOrTitleAreProvided_PostNotUpdated()
    {
        // arrange
        _blogContextMock.Setup(c => c.Posts).Returns(_dbSetMock.Object);
        var post = _posts.ElementAt(0);
        var dto = new UpdatePostDto { Title = string.Empty, Content = string.Empty };

        // act
        var actual = _repository.Update(post, dto);

        // assert
        actual.Should().NotBeNull();
        actual.Title.Should().Be(post.Title);
        actual.Content.Should().Be(post.Content);
    }
}