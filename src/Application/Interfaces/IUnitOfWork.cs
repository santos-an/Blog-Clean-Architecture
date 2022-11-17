using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    public ICommentRepository Comments { get; }
    public IPostRepository Posts { get; } 

    public Task CommitAsync();
}