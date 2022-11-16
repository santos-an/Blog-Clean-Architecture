namespace Application.Comments.Commands.CreateComment;

public record CreateCommentDto
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
}