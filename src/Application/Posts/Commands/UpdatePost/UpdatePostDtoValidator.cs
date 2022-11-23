using FluentValidation;

namespace Application.Posts.Commands.UpdatePost;

public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
{
    public UpdatePostDtoValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty();
        RuleFor(p => p.Title).MaximumLength(30);
        RuleFor(p => p.Content).MaximumLength(1200);
    }
}