using Application.Contracts.Auth;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher,
        IMapper mapper) : IAuthService
    {
        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var user = mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            user.Password = passwordHasher.Hash(user.Password);

            var existingUser = await userRepository.GetByEmail(user.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var token = tokenService.GenerateToken(user.Id, user.Email);
            user.Token = token;

            var result = await userRepository.AddAsync(user);
            return mapper.Map<CreateUserResponse>(result);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.GetByEmail(request.Email)
                ?? throw new InvalidOperationException("Invalid email or password.");

            if (!passwordHasher.Verify(request.Password, user.Password))
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            var token = tokenService.GenerateToken(user.Id, user.Email);

            user.Token = token;
            var userUpdated = await userRepository.UpdateAsync(user);

            return new LoginResponse { Token = userUpdated.Token };
        }
    }
}