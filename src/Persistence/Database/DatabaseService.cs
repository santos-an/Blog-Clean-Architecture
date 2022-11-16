﻿using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Comments;
using Persistence.Posts;

namespace Persistence.Database;

public class DatabaseService : DbContext, IDatabaseService
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    public async Task CommitAsync() => await SaveChangesAsync();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=BlogDatabase";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentsConfiguration());
    }
}