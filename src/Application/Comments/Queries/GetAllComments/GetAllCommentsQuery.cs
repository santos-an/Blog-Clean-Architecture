using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IGetAllCommentsQuery
{
    private readonly IDatabaseService _service;

    public GetAllCommentsQuery(IDatabaseService service) => _service = service;
    
    public Task<List<CommentListDto>> Execute()
    {
        return _service.Comments.Select(c => new CommentListDto()
        {
            Id = c.Id,
            PostId = c.PostId,
            Author = c.Author,
            Content = c.Content,
            CreationDate = c.CreationDate
        }).ToListAsync();
    }
}