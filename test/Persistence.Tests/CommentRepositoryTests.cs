using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Persistence.Database;

namespace Persistence.Tests;

public class CommentRepositoryTests
{
    private readonly IQueryable<Post> _posts;
    private readonly Mock<DbSet<Post>> _dbSetMock;
    private readonly Mock<BlogContext> _blogContextMock;
    
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CommentRepositoryTests()
    {
        _posts = DatabaseInitializer.Posts.AsQueryable();
        _dbSetMock = _posts.BuildMockDbSet();
        _blogContextMock = new Mock<BlogContext>();

        _commentRepository = new CommentRepository(_blogContextMock.Object);
        _postRepository = new PostRepository(_blogContextMock.Object);
        _unitOfWork = new UnitOfWork(_blogContextMock.Object, _commentRepository, _postRepository);
    }
}