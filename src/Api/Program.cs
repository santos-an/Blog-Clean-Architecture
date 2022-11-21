using System.Text.Json.Serialization;
using Api.Mapper;
using Api.Utils;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetComment;
using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetComments;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        ConfigureServices(services);
        ConfigureDi(services);

        var app = builder.Build();
        RunMigrations(app);
        ConfigureApp(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<BlogContext>();
        
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
        services.AddAutoMapper(typeof(MappingProfile));
    }

    private static void ConfigureDi(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        
        services.AddTransient<IGetAllCommentsQuery, GetAllCommentsQuery>();
        services.AddTransient<IGetCommentQuery, GetCommentQuery>();
        services.AddTransient<ICreateCommentCommand, CreateCommentCommand>();
        services.AddTransient<IUpdateCommentCommand, UpdateCommentCommand>();
        services.AddTransient<IDeleteCommentCommand, DeleteCommentCommand>();
        
        services.AddTransient<IGetAllPostsQuery, GetAllPostsQuery>();
        services.AddTransient<IGetPostQuery, GetPostQuery>();
        services.AddTransient<IGetCommentsQuery, GetCommentsQuery>();
        services.AddTransient<ICreatePostCommand, CreatePostCommand>();
        services.AddTransient<IUpdatePostCommand, UpdatePostCommand>();
        services.AddTransient<IDeletePostCommand, DeletePostCommand>();
    }

    private static void RunMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogContext>();

        context.Database.Migrate();
    }

    private static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}