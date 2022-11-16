using Domain;
using Microsoft.EntityFrameworkCore;


namespace Application.Interfaces;

public interface IDatabaseService
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
}