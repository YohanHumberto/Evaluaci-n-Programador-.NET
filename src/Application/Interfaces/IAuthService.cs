using Application.Contracts.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
