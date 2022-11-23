using FluentValidation;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(p => p.Title).NotEmpty().NotNull();
        RuleFor(p => p.Title).MaximumLength(30);

        RuleFor(p => p.Content).NotEmpty().NotNull();
        RuleFor(p => p.Content).MaximumLength(1200);
    }
}