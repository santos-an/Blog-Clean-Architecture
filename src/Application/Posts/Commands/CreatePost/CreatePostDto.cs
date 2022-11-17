namespace Application.Posts.Commands.CreatePost;

public record CreatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<CreateCommentDto> Comments { get; set; }
}

public record CreateCommentDto
{
    public string Content { get; set; }
    public string Author { get; set; }
}