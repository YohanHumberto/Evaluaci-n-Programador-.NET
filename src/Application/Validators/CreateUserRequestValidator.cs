using Application.Contracts.Auth;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserRequestValidator
        : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo es obligatorio.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("El correo no tiene un formato válido.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{9,}$")
                .WithMessage("La contraseña debe tener más de 8 caracteres e incluir mayúsculas, minúsculas, números y símbolos.");
        }
    }
}
