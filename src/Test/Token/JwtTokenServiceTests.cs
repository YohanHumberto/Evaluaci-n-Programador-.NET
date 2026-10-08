using Application.Services;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Test.Token;

public class JwtTokenServiceTests
{
    [Fact]
    public void GenerateToken_Contains_Claims()
    {
        var inMemory = new Dictionary<string, string?>
        {
            ["Jwt:Key"] = "MySuperSecretKey123b8f23nnf3nf23fadgsfb2y3ib672yb29hanf23ifnb2830infkasd23in4567890",
            ["Jwt:Issuer"] = "test",
            ["Jwt:Audience"] = "test",
            ["Jwt:ExpirationMinutes"] = "60"
        };

        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory)
            .Build();

        var svc = new JwtTokenService(config);
        var token = svc.GenerateToken(Guid.NewGuid(), "a@b.c");

        Assert.False(string.IsNullOrWhiteSpace(token));

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var sub = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);

        Assert.NotNull(sub);
        Assert.NotNull(email);
        Assert.Equal("a@b.c", email!.Value);
    }
}
