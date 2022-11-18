namespace Application.Comments.Queries.GetSingleComment;

public class CommentDto
{
    public Guid CommentId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
}