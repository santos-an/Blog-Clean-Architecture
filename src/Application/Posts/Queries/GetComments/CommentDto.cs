
using Application.Posts.Queries.GetAllPosts;

namespace Application.Posts.Queries.GetComments;

public record PostWithCommentsDto
{
    public Guid PostId { get; set; }
    public List<CommentListDto> Comments { get; set; }
}

public record CommentListDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
}