namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentDto
{
    public Guid Id { get; set; }
    public string? NewContent { get; set; }
    public string? NewAuthor { get; set; }
}