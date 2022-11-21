using Application.Comments.Queries.GetComment;

namespace Application.Posts.Queries.GetPost;

public record PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<CommentDto> Comments { get; set; }
    public DateTime CreationDate { get; set; }
}