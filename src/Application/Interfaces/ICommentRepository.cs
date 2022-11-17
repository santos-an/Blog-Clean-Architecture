using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.UpdateComment;
using Application.Comments.Queries.GetAllComments;
using Application.Comments.Queries.GetSingleComment;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<CommentListDto>> GetAll();
    public Task<CommentDto> Get(Guid id);
    public Task Create(CreateCommentDto dto);
    public Task<CommentDto> Update(UpdateCommentDto dto);
    public Task<bool> Delete(Guid id);
}