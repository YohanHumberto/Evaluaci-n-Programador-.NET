using Application.Contracts.Auth;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IValidator<CreateUserRequest> validator,
        IValidator<LoginRequest> loginValidator,
        IAuthService authService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var validation = await validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            // Lógica de creación
            var response = await authService.CreateUserAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var validation = await loginValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            // Lógica de login
            var response = await authService.LoginAsync(request);
            return Ok(response);
        }
    }
}
