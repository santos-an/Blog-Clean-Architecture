using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDatabaseService
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }

    public Task CommitAsync();
}