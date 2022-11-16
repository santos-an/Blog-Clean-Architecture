namespace Domain;

public record Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Comment> Comments { get; set; }
}