using Application.Interfaces;
using Persistence.Database;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogContext _context;
    
    public UnitOfWork(BlogContext context, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _context = context;
        Comments = commentRepository;
        Posts = postRepository;
    }
    
    public ICommentRepository Comments { get; }
    public IPostRepository Posts { get; }
    
    public async Task CommitAsync() => await _context.SaveChangesAsync();
}