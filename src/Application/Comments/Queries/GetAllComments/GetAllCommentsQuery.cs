using Application.Comments.Queries.GetSingleComment;
using Application.Interfaces;

namespace Application.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IGetAllCommentsQuery
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCommentsQuery(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
    public async Task<IEnumerable<CommentDto>> Execute()
    {
        var comments = await _unitOfWork.Comments.GetAll();
        return comments.Select(c => new CommentDto()
        {
            PostId = c.PostId,
            CommentId = c.Id,
            Author = c.Author,
            Content = c.Content,
            CreationDate = c.CreationDate
        }).ToList();
    }
}