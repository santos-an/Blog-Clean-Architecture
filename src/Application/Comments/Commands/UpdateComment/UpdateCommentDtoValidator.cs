using FluentValidation;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentDtoValidator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull();

        RuleFor(c => c.NewContent).NotEmpty().NotNull();
        RuleFor(c => c.NewContent).MaximumLength(120);
        
        RuleFor(p => p.NewAuthor).NotEmpty().NotNull();
        RuleFor(p => p.NewAuthor).MaximumLength(30);
    }
}