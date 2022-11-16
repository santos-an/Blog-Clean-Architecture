namespace Domain.Entities;

public record Comment
{
    public Guid Id { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
}