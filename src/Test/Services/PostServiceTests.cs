using Application.Contracts.Posts;
using Application.Interfaces;
using Application.Services;

namespace Test.Services;

public class PostServiceTests
{
    class FakeJsonPlaceholderService : IJsonPlaceholderService
    {
        public Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            return Task.FromResult(new PostResponse { Id = 1, Title = request.Title, Body = request.Body, UserId = request.UserId });
        }

        public Task<IEnumerable<PostResponse>> GetPostsAsync()
        {
            var list = new List<PostResponse> { new PostResponse { Id = 1, Title = "T", Body = "B", UserId = 1 } };
            return Task.FromResult<IEnumerable<PostResponse>>(list);
        }
    }

    [Fact]
    public async Task GetAllAsync_Returns_List()
    {
        var service = new PostService(new FakeJsonPlaceholderService());
        var result = await service.GetAllAsync();
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task CreatePostAsync_Returns_Created()
    {
        var service = new PostService(new FakeJsonPlaceholderService());
        var req = new CreatePostRequest { Title = "t", Body = "b", UserId = 1 };
        var result = await service.CreatePostAsync(req);
        Assert.NotNull(result);
        Assert.Equal(req.Title, result.Title);
    }
}
