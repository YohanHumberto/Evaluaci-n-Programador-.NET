using Application.Contracts.Auth;
using FluentValidation;

namespace Application.Validators
{
    public class LoginRequestValidator
        : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo es obligatorio.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("El correo no tiene un formato válido.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria.");
        }
    }
}