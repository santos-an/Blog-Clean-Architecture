using FluentValidation;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentDtoValidator : AbstractValidator<Posts.Commands.CreatePost.CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(c => c.Author).NotEmpty().NotNull();
        RuleFor(p => p.Author).MaximumLength(30);
        
        RuleFor(c => c.Content).NotEmpty().NotNull();
        RuleFor(p => p.Content).MaximumLength(120);
    }
}