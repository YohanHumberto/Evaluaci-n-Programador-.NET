using Application.Contracts.Auth;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using PruebaTecnicaApi.MapperProfiles;

namespace Test.Auth;

public class AuthServiceTests
{
    class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _store = new();

        public Task<User> AddAsync(User entity)
        {
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> RemoveAsync(User entity)
        {
            var removed = _store.Remove(entity);
            return Task.FromResult(removed);
        }

        public Task<User> UpdateAsync(User entity)
        {
            var idx = _store.FindIndex(u => u.Id == entity.Id);
            if (idx >= 0) _store[idx] = entity;
            return Task.FromResult(entity);
        }

        public Task<List<User>> GetAll() => Task.FromResult(new List<User>(_store));

        public Task<User?> GetById(Guid id) => Task.FromResult(_store.Find(u => u.Id == id));

        public Task<User?> GetByEmail(string email) => Task.FromResult(_store.Find(u => u.Email == email));
    }

    class FakeTokenService : ITokenService
    {
        public string GenerateToken(Guid userId, string email) => "fixed-token";
    }

    class FakePasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => "hashed-" + password;
        public bool Verify(string password, string passwordHash) => passwordHash == "hashed-" + password;
    }

    [Fact]
    public async Task CreateUserAsync_Creates_User_And_Returns_Response()
    {
        var repo = new FakeUserRepository();
        var tokenSvc = new FakeTokenService();
        var hasher = new FakePasswordHasher();

        var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
        var mapper = mapperCfg.CreateMapper();

        var svc = new AuthService(repo, tokenSvc, hasher, mapper);

        var req = new CreateUserRequest { Name = "n", Email = "e@x.com", Password = "p" };
        var res = await svc.CreateUserAsync(req);

        Assert.NotNull(res);
        Assert.Equal(req.Email, res.Email);
        Assert.Equal("fixed-token", res.Token);
    }

    [Fact]
    public async Task LoginAsync_Returns_Token_When_Credentials_Are_Correct()
    {
        var repo = new FakeUserRepository();
        var tokenSvc = new FakeTokenService();
        var hasher = new FakePasswordHasher();

        var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
        var mapper = mapperCfg.CreateMapper();

        var svc = new AuthService(repo, tokenSvc, hasher, mapper);

        // create user in repo
        var user = new User { Id = Guid.NewGuid(), Name = "n", Email = "a@b.c", Password = hasher.Hash("p") };
        await repo.AddAsync(user);

        var loginReq = new LoginRequest { Email = user.Email, Password = "p" };
        var loginRes = await svc.LoginAsync(loginReq);

        Assert.NotNull(loginRes);
        Assert.Equal("fixed-token", loginRes.Token);
    }
}
