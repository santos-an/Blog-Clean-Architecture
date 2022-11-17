namespace Application.Posts.Commands.UpdatePost;

public record UpdatePostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}