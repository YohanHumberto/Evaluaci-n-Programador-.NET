using Application.Services;

namespace Test.Security;

public class BCryptPasswordHasherTests
{
    [Fact]
    public void Hash_And_Verify_Works()
    {
        var hasher = new BCryptPasswordHasher();
        var password = "MyP@ssw0rd";
        var hash = hasher.Hash(password);

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.True(hasher.Verify(password, hash));
        Assert.False(hasher.Verify("bad", hash));
    }
}
