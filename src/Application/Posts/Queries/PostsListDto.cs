namespace Application.Posts.Queries;

public record PostsListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<CommentDto> Comments { get; set; }
    public DateTime CreationDate { get; set; }
}

public record CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
}