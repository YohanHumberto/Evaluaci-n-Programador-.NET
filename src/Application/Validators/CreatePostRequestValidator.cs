using Application.Contracts.Posts;
using FluentValidation;

namespace Application.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.UserId)
                  .InclusiveBetween(1, 10)
                   .WithMessage("El UserId debe estar entre 1 y 10.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("El título es obligatorio.")
                .MaximumLength(200)
                .WithMessage("El título no puede superar los 200 caracteres.");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage("El contenido es obligatorio.");
        }
    }
}
