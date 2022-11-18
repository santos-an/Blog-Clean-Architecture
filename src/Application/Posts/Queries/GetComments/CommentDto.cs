namespace Application.Posts.Queries.GetComments;

public record CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
}