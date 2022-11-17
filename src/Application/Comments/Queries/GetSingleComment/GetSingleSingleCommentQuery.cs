using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetSingleComment;

public class GetSingleSingleCommentQuery : IGetSingleCommentQuery
{
    private readonly IUnitOfWork _service;
    
    public GetSingleSingleCommentQuery(IUnitOfWork service) => _service = service;

    public async Task<CommentDto> Execute(Guid id)
    {
        return await _service.Comments.Get(id);
    }
}