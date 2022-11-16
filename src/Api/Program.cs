using System.Text.Json.Serialization;
using Api.Utils;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetByPostId;
using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;
using Application.Posts.Queries.GetAllPosts;
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
        services.AddDbContext<DatabaseService>();
        
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
    }

    private static void ConfigureDi(IServiceCollection services)
    {
        services.AddSingleton<IDatabaseService, DatabaseService>();
        
        services.AddTransient<IGetAllPostsQuery, GetAllPostsQuery>();
        services.AddTransient<IGetAllCommentsQuery, GetAllCommentsQuery>();
        services.AddTransient<IGetSingleCommentQuery, GetSingleSingleCommentQuery>();
        services.AddTransient<ICreateCommentCommand, CreateCommentCommand>();
        services.AddTransient<IUpdateCommentCommand, UpdateCommentCommand>();
        services.AddTransient<IDeleteCommentCommand, DeleteCommentCommand>();
        services.AddTransient<IGetCommentByPostIdQuery, GetCommentByPostIdQuery>();
    }

    private static void RunMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseService>();

        context.Database.Migrate();
    }

    private static void ConfigureApp(WebApplication app)
    {
        // Configure the HTTP request pipeline.
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